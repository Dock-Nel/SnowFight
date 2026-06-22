using UnityEngine;

public class BotController : MonoBehaviour
{
    private GameManager gameManager;
    public float health;
    public GameObject FrozenGingerbread;

    [SerializeField] GameObject Sprite1;
    [SerializeField] GameObject Sprite2;

    private void Start()
    {
        gameManager = Object.FindAnyObjectByType<GameManager>();
    }
    public void Update()
    {
        if (health == 20)
        {
            Sprite1.SetActive(true);
            Sprite2.SetActive(false);
        }
        else if (health == 10)
        {
            Sprite1.SetActive(false);
            Sprite2.SetActive(true);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health is now at: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        gameManager.RemoveBotFromList(this.gameObject);
        GameObject spawnedFrozen = Instantiate(FrozenGingerbread, transform.position, transform.rotation);
        spawnedFrozen.SetActive(true);
        gameManager.RegisterFrozenStatue(spawnedFrozen);
        Destroy(gameObject);    
    }
}