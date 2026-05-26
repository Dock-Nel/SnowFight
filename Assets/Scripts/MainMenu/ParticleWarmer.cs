using UnityEngine;

public class ParticleWarmer : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    void Awake()
    {
        ParticleSystem.Play();
        Time.timeScale = 1.0f;
    }
}
