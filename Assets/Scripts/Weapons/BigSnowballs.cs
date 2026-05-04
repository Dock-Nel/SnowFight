using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BigSnowballs : MonoBehaviour
{
    [SerializeField] private ParticleSystem snowExplosionPrefab;
    private bool hasExploded = false;
    private BotController bot;

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
            BotController bot = collision.gameObject.GetComponent<BotController>();
            if (bot != null)
            {
                bot.TakeDamage(30);
                // don't die if ennemies with bigger health are implemented
            }
        }     
        
    }
}