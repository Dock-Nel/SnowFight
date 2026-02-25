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
        if (PlayerDetection)
        {
            Linecast();
        }

        //AI movements 
        if (Move || PlayerDetection && PlayerDistance > Range/2)
        {
            agent.destination = Player.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    //Linecast function
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
}
