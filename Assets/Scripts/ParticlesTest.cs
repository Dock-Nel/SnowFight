using UnityEngine;

public class ParticlesTest : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision avec : " + other.name);
    }
}
