using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("--MUSIC--")]

    [SerializeField] private AudioSource _MainMusic;
    private bool _MainMusicPlaying = true;

    [Header("--SFX--")]

    [Header("game")]
    [SerializeField] private AudioSource _SnowballShot;
    [SerializeField] private AudioSource _FootStepSource;
    [SerializeField] private AudioClip[] _FootStepClip;
    [SerializeField] private AudioSource _SnowballExplosion;
    [SerializeField] private AudioSource _WinRound;

    [Header("ui")]
    [SerializeField] private AudioSource _ClickUI;
    [SerializeField] private AudioSource _ClickUIPause;
    [SerializeField] private AudioSource _GrabBonus1;
    [SerializeField] private AudioSource _GrabBonus2;

    private void Start()
    {
        AssignButtonSounds();
    }

    public void AssignButtonSounds()
    {
        Button[] allButtons = Object.FindObjectsByType<Button>(FindObjectsInactive.Include);
        foreach (Button btn in allButtons)
        {
            btn.onClick.RemoveListener(PlayClickUI);
            btn.onClick.AddListener(PlayClickUI);
        }

    }

    private void PlayClickUI()
    {
        if (_ClickUI != null)
        {
            _ClickUI.pitch = Random.Range(0.80f, 1.15f); 
            _ClickUI.PlayOneShot(_ClickUI.clip);
        }
    }

    public void StopMainMusic()
    {
        if (_MainMusicPlaying)
        {
            _MainMusic.Stop();
            _MainMusicPlaying = false;
        }
    }
    public void StartMainMusic()
    {
        if (!_MainMusicPlaying)
        {
            _MainMusic.Play();
            _MainMusicPlaying = false;
        }
    }

    public void PlayRandomFootstep()
    {
        int clip = Random.Range(0, _FootStepClip.Length);
        _FootStepSource.pitch = Random.Range(0.9f, 1.1f);
        _FootStepSource.PlayOneShot(_FootStepClip[clip]);
    }

    public void PlaySnowballShotRandomPitch()
    {
        if (_SnowballShot != null && _SnowballShot.clip != null)
        {
            _SnowballShot.pitch = Random.Range(0.60f, 1.15f);
            _SnowballShot.PlayOneShot(_SnowballShot.clip);
        }
    }

    public void PlaySnowballShotRandomPitchAI(Vector3 spawnPosition)
    {
        if (_SnowballShot != null && _SnowballShot.clip != null)
        {
            _SnowballShot.pitch = Random.Range(0.60f, 1.15f);
            AudioSource temp3DSound = Instantiate(_SnowballShot, spawnPosition, Quaternion.identity);
            temp3DSound.volume = _SnowballShot.volume * 0.5f;
            temp3DSound.Play();
            Destroy(temp3DSound.gameObject, _SnowballShot.clip.length);
        }
    }

    public void PlaySnowballExplosionRandomPitch(Vector3 spawnPosition)
    {
        if (_SnowballExplosion != null && _SnowballExplosion.clip != null)
        {
            _SnowballExplosion.pitch = Random.Range(0.60f, 1.15f);
            AudioSource temp3DSound = Instantiate(_SnowballExplosion, spawnPosition, Quaternion.identity);
            temp3DSound.Play();
            Destroy(temp3DSound.gameObject, _SnowballExplosion.clip.length);
        }
    }
}