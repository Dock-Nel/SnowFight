using UnityEngine;

public class TestParticles : MonoBehaviour
{
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
    }
}