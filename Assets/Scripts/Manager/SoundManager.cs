using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private bool isMusicOff = false;
    private bool isSoundOff = false;

    [SerializeField] private AudioClip TapClip;
    [SerializeField] private AudioClip PreyCollect;
    [SerializeField] private AudioClip BgClip;
    [SerializeField] private AudioClip playerDieClip;
    [SerializeField] private AudioClip fishSwimClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
    }

    // ---------- MUSIC ----------
    public void ToggleMusic()
    {
        isMusicOff = !isMusicOff;

        if (isMusicOff)
            musicSource.Pause();
        else
            musicSource.UnPause();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (isMusicOff) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void Background() => PlayMusic(BgClip);
    public void FishSwim() => PlayMusic(fishSwimClip);

    public bool IsMusicOff() => isMusicOff;

    // ---------- SOUND (SFX) ----------
    public void ToggleSound()
    {
        isSoundOff = !isSoundOff;
    }

    public void PlaySound(AudioClip clip)
    {
        if (isSoundOff) return;

        sfxSource.PlayOneShot(clip);
    }

    public void Tap() => PlaySound(TapClip);
    public void Prey() => PlaySound(PreyCollect);
    public void PlayerDieSound() => PlaySound(playerDieClip);

    public bool IsSoundOff() => isSoundOff;

    public bool IsMusicOn()
    {
        return !isMusicOff;
    }

}
