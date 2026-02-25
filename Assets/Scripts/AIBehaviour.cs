using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class AIBehaviour : MonoBehaviour
{
    //Pathfinding
    NavMeshAgent agent;

    //Player
    public GameObject Player;

    //Raycast call
    Ray ray;
    RaycastHit hit;

    //Variables
    [SerializeField] bool Alive = true;
    [SerializeField] int Snowball = 3;

    [SerializeField] bool PlayerDetection = false;
    [SerializeField] bool Aggressivity = false;
    [SerializeField] float PlayerDistance;

    //Constants 
    [SerializeField] int Speed;
    [SerializeField] int Gravity;
    [SerializeField] int Range = 20;
    

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
            Raycast();
            PlayerDetection = true;
        }
        else
        {
            PlayerDetection = false;
        }
    }

    //Raycast function
    void Raycast()
    {
        //Raycast Calculation
        ray = new Ray(transform.position, Player.transform.position);
        Debug.DrawLine(transform.position, Player.transform.position, Color.red); //Makes the Ray visible, debug feature only

        if (!Physics.Linecast(transform.position, Player.transform.position)) //If Ray reaches Player
        {
            Aggressivity = true;
            agent.destination = transform.position;

            Debug.Log("test");
        }
        else
        {
            agent.destination = Player.transform.position;

            Debug.Log("Player in range, not detected");
        }
    }
}
