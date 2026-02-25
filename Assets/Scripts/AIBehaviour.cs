using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class AIBehaviour : MonoBehaviour
{
    //Pathfinding
    NavMeshAgent agent;

    //Player
    public GameObject Player;

    //Variables
    [SerializeField] bool Alive = true;
    [SerializeField] int Snowball = 3;

    [SerializeField] bool PlayerDetection = false;
    [SerializeField] bool Move = false;
    [SerializeField] bool Aggressivity = false;
    [SerializeField] float PlayerDistance;

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
            case State.Idle:
                if (Snowball < 3 /*&& !Reload()*/)
                {
                    Reload();
                }
                else if (Snowball == 3)
                {
                    //Patrol movements
                }
                break;

            case State.Move:
                Linecast();
                Movements();
                break;

            case State.Attack:
                Movements();
                if (Snowball > 0)
                {
                    Shoot();
                }
                else
                {
                    Reload();
                }
                break;
        }
    }

    //AI Movements
    void Movements()
    {
        //Velocity Movements
        if (Move || PlayerDistance > Range / 2)
        {
            agent.destination = Player.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }

        //Gravity

        //Rotation


    }

    //Snowball
    void Shoot()
    {
        Snowball--;
        currentState = State.Move;
    }

    //Reload ammos
    void Reload()
    {
        Snowball++;
    }

    //Linecast function to determine if the AI sees the player or not
    void Linecast()
    {
        Debug.DrawLine(transform.position, Player.transform.position, Color.red); //Makes the Ray visible, debug feature only

        if (!Physics.Linecast(transform.position, Player.transform.position)) //If Ray reaches Player
        {
            Move = false;
        }
        else
        {
            Move = true;
        }
    }

    //Death detection
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Amo")
        {
            Destroy(this);
        }
    }
}
