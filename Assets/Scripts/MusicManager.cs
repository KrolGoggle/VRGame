using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource _audioSource;
    public AudioClip mainThemeMusic;
    public AudioClip startGameMusic;
    public AudioClip inGameMusic;
    public static MusicManager instance;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource component not found on MusicManager GameObject.");
        }
        instance = this;
    }
    void Start()
    {
        
    }
    public void PlayMainTheme()
    {
        // plynne przejscie wyciszajac muzyke i podglasniajac kolejna
        StartCoroutine(FadeOutAndPlayNewClip(mainThemeMusic));
    }

    public void PlayStartGameMusic()
    {
        // plynne przejscie wyciszajac muzyke i podglasniajac kolejna
        StartCoroutine(FadeOutAndPlayNewClip(startGameMusic));
    }

    public void PlayInGameMusic()
    {
        // plynne przejscie wyciszajac muzyke i podglasniajac kolejna
        StartCoroutine(FadeOutAndPlayNewClip(inGameMusic));
    }

    private IEnumerator FadeOutAndPlayNewClip(AudioClip newClip)
    {
        if (_audioSource != null)
        {
            // Fade out the current music
            float fadeOutTime = 1f; // czas wyciszenia
            float startVolume = _audioSource.volume;

            for (float t = 0; t < fadeOutTime; t += Time.deltaTime)
            {
                _audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeOutTime);
                yield return null;
            }

            _audioSource.Stop();
            _audioSource.clip = newClip;
            _audioSource.Play();

            // Fade in the new music
            float fadeInTime = 1f; // czas podglosnienia
            for (float t = 0; t < fadeInTime; t += Time.deltaTime)
            {
                _audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeInTime);
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
