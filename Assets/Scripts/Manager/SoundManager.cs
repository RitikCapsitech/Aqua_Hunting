using UnityEngine;

public class SoundManager : MonoBehaviour

{

    public static SoundManager Instance;

    private AudioSource music_AudioSource;

    private AudioSource sound_AudioSource;

    private bool isSFXoff;

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

            Destroy(gameObject);

        music_AudioSource = gameObject.AddComponent<AudioSource>();

        sound_AudioSource = gameObject.AddComponent<AudioSource>();

        music_AudioSource.playOnAwake = false;

        sound_AudioSource.playOnAwake = false;

    }

    private void PlayBgSound(AudioClip clip)

    {

        music_AudioSource.clip = clip;

        music_AudioSource.loop = true;

        music_AudioSource.Play();

    }

    private void PlaySFxSound(AudioClip clip)

    {

        if (isSFXoff) return;

        sound_AudioSource.clip = clip;

        sound_AudioSource.Play();

    }

    public void BgSoundControl(bool isPlay)

    {

        if (isPlay) music_AudioSource.Stop();

        else music_AudioSource.Play();

    }

    public void SfXControl(bool isPlay)

    {

        isSFXoff = !isPlay;

    }
    public void FishSwim()
    {
        PlayBgSound(fishSwimClip);
    }
    public void Tap()
    {
        PlaySFxSound(TapClip);
    }


    public void Prey()
    {
        PlaySFxSound(PreyCollect);
    }



    public void Background()
    {
        PlayBgSound(BgClip);
    }

    public void PlayerDieSound()
    {
        PlaySFxSound(playerDieClip);

    }
    public void StopMusic()
    {
        music_AudioSource.Stop();
    }



}
