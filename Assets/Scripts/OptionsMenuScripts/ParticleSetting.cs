using UnityEngine;

public class ParticleSetting : MonoBehaviour
{
    ParticleSystem ParticleSystem;
    SettingsValues Settings;

    void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        Settings = FindAnyObjectByType<SettingsValues>();
    }

    private void Update()
    {
        if (Settings == null)
        {
            Debug.Log("No Settings Found !");
        }
        else
        {
            if (Settings.SnowEffectBool)
            {
                ParticleSystem.Play();
            }
            else
            {
                ParticleSystem.Clear();
            }
        }
    }
}
