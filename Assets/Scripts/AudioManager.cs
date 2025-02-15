using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 싱글턴 인스턴스
    public static AudioManager Instance;

    [SerializeField]
    private AudioClip testBGM;

    // 배경음악과 효과음을 위한 AudioSource
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    // 초기화
    void Awake()
    {
        // 싱글턴 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // AudioSource 컴포넌트 추가
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        // 기본 설정
        bgmSource.loop = true; // 배경음악은 루프 재생
        bgmSource.volume = 0.5f; // 배경음 볼륨
        sfxSource.volume = 1.0f; // 효과음 볼륨

        PlayBGM(testBGM);
    }

    // 배경음악 재생
    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.Play();
        }
    }

    // 효과음 재생
    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }
}