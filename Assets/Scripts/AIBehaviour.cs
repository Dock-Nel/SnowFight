using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;


public class AIBehaviour : MonoBehaviour
{
    //Components
    NavMeshAgent agent;
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    [SerializeField] Transform Destination;

    //Player
    public GameObject Player; 
    [SerializeField] private Rigidbody snowball;
    [SerializeField] private Transform shootingDisctrict;

    //Variables
    [SerializeField] int Snowball = 3;

    [SerializeField] bool PlayerDetection = false;
    [SerializeField] bool MoveTowardsPlayer = false;
    [SerializeField] bool Aggressivity = false;
    [SerializeField] float PlayerDistance;
    [SerializeField] float TargetRotation;

    [SerializeField] bool CRRunning;
    //Constants 
    [SerializeField] int Speed;
    [SerializeField] int Gravity;
    [SerializeField] int Range;

    private enum State { Idle, Move, Attack }
    [SerializeField] private State currentState = State.Idle;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //Player detection system
        PlayerDistance = Vector3.Distance(transform.position, Player.transform.position);
        if (PlayerDistance < Range)
        {
            PlayerDetection = true;
        }
        else if (PlayerDistance > Range * 1.5)
        {
            PlayerDetection = false;
            MoveTowardsPlayer = false;
        }

        if (PlayerDetection && Aggressivity)
        {
            currentState = State.Attack;
        }
        else if (PlayerDetection)
        {
            currentState = State.Move;
        }
        else if (!PlayerDetection)
        {
            currentState = State.Idle;
        }

        //Behaviour Switch
        switch (currentState)
            {
                case State.Idle: //When the player is *NOT detected*
                    if (Snowball < 3 && !CRRunning)
                    {
                        StartCoroutine(Reload());
                    }
                    else if (Snowball == 3)
                    {
                        Movements();
                    }
                    break;

                case State.Move: //When the player is *detected*
                    if (PlayerDistance > Range / 2 || Linecast() == false)
                    {
                        MoveTowardsPlayer = true;
                    }
                    else
                    {
                        MoveTowardsPlayer = false;
                        Aggressivity = true;
                    }
                    Movements();
                    break;

                case State.Attack: //When the AI attacks the player

                    if (Snowball > 0 && !CRRunning)
                    {
                        StartCoroutine(Shoot());
                    }
                    else if (!CRRunning)
                    {
                        StartCoroutine(Reload());
                    }
                    Aggressivity = false;
                    Movements();
                    break;
            }
    }

    //AI Movements
    void Movements()
    {
        //Velocity Movements
        if (MoveTowardsPlayer)
        {
            agent.destination = Player.transform.position;
        }
        else if (!MoveTowardsPlayer && !PlayerDetection)
        {
            if (Destination == null || agent.remainingDistance <= 5)
            {
                Destination = wayPoints[UnityEngine.Random.Range(0, wayPoints.Count)];
            }
            agent.destination = Destination.position;
        }
        else
        {
            agent.destination = transform.position;
        }
        

        //Rotation only when player is in range (as the sprite only faces the player, only useful because of the snowball cast)
        if (PlayerDetection)
        {
            TargetRotation = Mathf.Atan2(Player.transform.position.x - transform.position.x, Player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, TargetRotation, 0f);
        }
    }

    //Snowball
    IEnumerator Shoot()
    {
        Debug.Log("Shooting...");
        CRRunning = true;
        Rigidbody clone = Instantiate(snowball, shootingDisctrict.position, shootingDisctrict.rotation);
        Snowballs SnowballScript = clone.GetComponent<Snowballs>();
        SnowballScript.Source = "Bot";
        clone.gameObject.SetActive(true);
        clone.linearVelocity = transform.TransformDirection((Vector3.forward + (Vector3.up / (5 - (PlayerDistance/5)))) * 10);
        yield return new WaitForSeconds(3); //Cooldown so the AI doesn't become a AK47
        Snowball--;
        currentState = State.Move;
        CRRunning = false;
    }

    //Reload ammos
    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        CRRunning = true;
        yield return new WaitForSeconds(2);
        Debug.Log("Reload");
        Snowball++;
        CRRunning = false;
    }

    //Linecast function to determine if the AI sees the player or not
    bool Linecast()
    {
        Debug.DrawLine(transform.position, Player.transform.position, Color.red); //Makes the Ray visible, debug feature only

        if (!Physics.Linecast(transform.position, Player.transform.position)) //If Ray reaches Player
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
