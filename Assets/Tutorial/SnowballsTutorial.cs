using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SnowballsTutorial : MonoBehaviour
{
    [SerializeField] private ParticleSystem snowExplosionPrefab;
    private bool hasExploded = false;
    private PlayerController player;
    private BotControllerNoGameManager bot;

    private void OnCollisionEnter(Collision collision)
    {

        if (hasExploded) return;

        if (!collision.gameObject.CompareTag("NoCollision"))
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
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            BotControllerNoGameManager bot = collision.gameObject.GetComponent<BotControllerNoGameManager>();
            if (player != null)
            {
                player.TakeDamage(10);
            }
            else if (bot != null)
            {
                bot.TakeDamage(10);
            }
        }     
        
    }
}