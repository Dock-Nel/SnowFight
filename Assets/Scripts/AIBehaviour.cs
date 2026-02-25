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
    [SerializeField] bool PlayerDetection = false;
    [SerializeField] bool Agressivity = false;

    [SerializeField] int Speed;
    [SerializeField] int Gravity;
    [SerializeField] float PlayerDistance;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        PlayerDistance = Vector3.Distance(transform.position, Player.transform.position);
        if (PlayerDistance < 10)
        {
            PlayerDetected();
        }
    }

    void PlayerDetected()
    {
        ray = new Ray(transform.position, Player.transform.position);
        Debug.DrawLine(transform.position, Player.transform.position, Color.red);
        if (!Physics.Raycast(ray, out hit))
        {
            Debug.Log("Player Detected");
        }
        else
        {
            Debug.Log("Player in range, not detected");
        }  
    }
}
