using JetBrains.Annotations;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BigSnowballs : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private ParticleSystem snowExplosionPrefab;
    private bool hasExploded = false;
    private PlayerController player;
    private BotController bot;
    private BotControllerNoGameManager botDumb;
    public string Source;

    private void OnCollisionEnter(Collision collision)
    {

        if (hasExploded) return;

        if (!collision.gameObject.CompareTag("NoCollision"))
        {
            //Debug.Log(collision.gameObject.name);
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            BotController bot = collision.gameObject.GetComponent<BotController>();
            BotControllerNoGameManager botDumb = collision.gameObject.GetComponent<BotControllerNoGameManager>();
            if (player != null && Source == "Bot")
            {
                player.TakeDamage(30);
                Explodes();
            }
            else if (bot != null && Source == "Player")
            {
                bot.TakeDamage(30);
                Explodes();
            }
            else if (botDumb != null)
            {
                botDumb.TakeDamage(30);
                Explodes();
            }
            else
            {
                Explodes();
            }
        }
    }

    private void Explodes()
    {
        hasExploded = true;
        ParticleSystem particles = Instantiate(
            snowExplosionPrefab,
            transform.position,
            Quaternion.identity
        );
        particles.Play();
        Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}