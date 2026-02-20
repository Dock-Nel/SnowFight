using UnityEngine;

public class Snowballs : MonoBehaviour
{
    [SerializeField] private ParticleSystem snowExplosionPrefab;
    private bool hasExploded = false;
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
        }
    }
}