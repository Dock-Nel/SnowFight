using UnityEngine;

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
}