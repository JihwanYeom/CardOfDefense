using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static AudioManager Instance;

    [SerializeField]
    private AudioClip testBGM;

    // ������ǰ� ȿ������ ���� AudioSource
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    // �ʱ�ȭ
    void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� ����
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // AudioSource ������Ʈ �߰�
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        // �⺻ ����
        bgmSource.loop = true; // ��������� ���� ���
        bgmSource.volume = 0.5f; // ����� ����
        sfxSource.volume = 1.0f; // ȿ���� ����

        PlayBGM(testBGM);
    }

    // ������� ���
    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.Play();
        }
    }

    // ȿ���� ���
    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }
}