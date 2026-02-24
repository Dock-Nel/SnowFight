using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class AIBehaviour : MonoBehaviour
{
    //Pathfinding
    public NavMeshAgent agent;

    //Raycast call
    Ray ray;
    RaycastHit hit;

    //Variables
    bool PlayerDetection = false;
    bool Agressivity = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            ray = new Ray(transform.position, other.transform.position);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Player Detected");
                Vector3 destination = other.transform.position;
                agent.destination = destination;
            }
            else
            {
                Debug.Log("Player in range, not detected");
            }  
        }
    }
}
