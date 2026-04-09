using System;
using System.Collections;
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

    //Player
    public GameObject Player;

    //Variables
    [SerializeField] int Snowball = 3;
    [SerializeField] int Health = 3;

    [SerializeField] bool PlayerDetection = false;
    [SerializeField] bool MoveTowardsPlayer = false;
    [SerializeField] float PlayerDistance;
    [SerializeField] float TargetRotation;

    [SerializeField] bool CRrunning;
    //Constants 
    [SerializeField] int Speed;
    [SerializeField] int Gravity;
    [SerializeField] int Range;

    private enum State { Idle, Move, Attack }
    private State currentState = State.Idle;

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
        else if (PlayerDistance > Range*1.5)
        {
            PlayerDetection = false;
            MoveTowardsPlayer = false;
        }

        if (PlayerDetection && currentState != State.Attack)
        {
            currentState = State.Move;
        }
        else if (!PlayerDetection && currentState != State.Attack)
        {
            currentState = State.Idle;
        }

        //Behaviour Switch
        switch(currentState)
        {
            case State.Idle: //When the player is *NOT detected*
                if (Snowball < 3 && !CRrunning)
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
                }
                Movements();
                break;

            case State.Attack: //When the AI attacks the player
                Movements();
                if (Snowball > 0)
                {
                    StartCoroutine(Shoot());
                }
                else
                {
                    StartCoroutine(Reload());
                }

                break;
        }

        if (Health <= 0)
        {
            Death();
        }
    }

    //AI Movements
    void Movements()
    {
        //Velocity Movements
        if (MoveTowardsPlayer)
        {
            agent.destination = Player.transform.position;
            Debug.Log("Moving Towards Player");
        }
        else
        {
            agent.destination = transform.position;
        }

        //Rotation only when player is in range (as the sprite only faces the player, only useful because of the snowball cast)
        if (PlayerDetection)
        {
            TargetRotation = Mathf.Atan2(transform.position.x - Player.transform.position.x, transform.position.z - Player.transform.position.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, TargetRotation, 0f);
        }
    }

    //Snowball
    IEnumerator Shoot()
    {
        Debug.Log("Shooting...");
        CRrunning = true;
        yield return new WaitForSeconds(5);
        Snowball--;
        currentState = State.Move;
        CRrunning = false;
    }

    //Reload ammos
    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        CRrunning = true;
        yield return new WaitForSeconds(5);
        Debug.Log("Reload");
        Snowball++;
        CRrunning = false;
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

    //Collision with a snowball detection
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Amo")
        {
            Health--;
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
