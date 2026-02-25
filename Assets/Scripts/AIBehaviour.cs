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
        else if (!PlayerDetection && Snowball < 3)//If the AI doesn't detect the player, then the AI reloads completely for another eventual fight
        {
            Reload();
        }
        MovementsAndShoot();
    }

    //AI Movements
    void MovementsAndShoot()
    {
        //Movements
        if (Move || PlayerDetection && PlayerDistance > Range / 2)
        {
            agent.destination = Player.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }

        //Shoot
        if (Snowball > 0)
        {
            Snowball--;
        }
        else if (Snowball == 0) //Reload if no ammo
        {
            Reload();
        }
    }

    //reload ammos
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
