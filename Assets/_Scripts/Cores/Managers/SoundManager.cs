using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤
    public static SoundManager instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource _bgmSource; // 배경음악
    [SerializeField] AudioSource _sfxSource; // 효과음

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 배경음악 재생
    public void PlayBGM(AudioClip clip, float volume = 0.5f)
    {
        _bgmSource.clip = clip;
        _bgmSource.volume = volume;
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    // 효과음 재생
    public void PlaySFX(AudioClip clip, float volume = 0.5f)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }
}
