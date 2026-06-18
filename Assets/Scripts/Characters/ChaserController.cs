using UnityEngine;

public class ChaserController : MonoBehaviour
{
    private GameManager gameManager;
    public float health = 1f; 
    public GameObject FrozenGingerbread;

    private void Start()
    {
        gameManager = Object.FindAnyObjectByType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameManager != null)
        {
            gameManager.RemoveBotFromList(this.gameObject);

            GameObject spawnedFrozen = Instantiate(FrozenGingerbread, transform.position, transform.rotation);
            spawnedFrozen.SetActive(true);
            gameManager.RegisterFrozenStatue(spawnedFrozen);
        }
        Destroy(gameObject);
    }
}