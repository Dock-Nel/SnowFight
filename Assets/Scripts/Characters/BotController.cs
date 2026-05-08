using UnityEngine;
using TMPro;
using System.Collections;

public class BotController : MonoBehaviour
{
    private GameManager gameManager;
    public float health = 30;
    public GameObject FrozenGingerbread;

    private void Start()
    {
        gameManager = Object.FindAnyObjectByType<GameManager>();
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
        Instantiate(FrozenGingerbread, transform.position, transform.rotation).SetActive(true);
        Destroy(gameObject);    
    }
}