using UnityEngine;

public class Snowballs : MonoBehaviour
{
    [SerializeField] private ParticleSystem snowExplosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("NoCollision"))
        {
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
}