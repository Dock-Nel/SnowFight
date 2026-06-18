using UnityEngine;
using UnityEngine.AI;

public class ChaserBehaviour : MonoBehaviour
{
    NavMeshAgent agent;
    private GameObject player;
    private ChaserController controller;

    [SerializeField] private float chaseSpeed = 6f; 
    [SerializeField] private int damageToPlayer = 10;

    public GameObject Shadow;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<ChaserController>();
    }

    void Start()
    {
        PlayerController playerScript = Object.FindAnyObjectByType<PlayerController>();
        if (playerScript != null)
        {
            player = playerScript.gameObject;
        }

        if (agent != null)
        {
            agent.speed = chaseSpeed;
            agent.angularSpeed = 120f;
        }
    }

    void Update()
    {
        if (Shadow != null)
        {
            Ray downRay = new Ray(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -Vector3.up);
            if (Physics.Raycast(downRay, out RaycastHit hitShadow))
            {
                Vector3 hitPosition = hitShadow.point;
                hitPosition.y += 0.05f;
                Shadow.transform.position = hitPosition;
            }
        }

        if (agent != null && player != null)
        {
            agent.destination = player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController targetPlayer = other.gameObject.GetComponent<PlayerController>();

        if (targetPlayer != null)
        {
            targetPlayer.TakeDamage(damageToPlayer);

            // chaser death
            if (controller != null)
            {
                controller.TakeDamage(999);
            }
        }
    }
}