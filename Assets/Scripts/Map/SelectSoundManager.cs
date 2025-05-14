using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SelectSoundManager : MonoBehaviour
{
    public static SelectSoundManager Instance { get; private set; }

    [Header("Select SFX")]
    public AudioClip selectClip;    // Inspector에 드래그할 효과음 파일

    AudioSource _src;

    void Awake()
    {
        // 싱글톤 패턴 (씬에 하나만 존재하도록)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _src = GetComponent<AudioSource>();
            // 2D 사운드로 설정
            _src.spatialBlend = 0f;
            _src.playOnAwake = false;
            _src.loop = false;
        }
        else Destroy(gameObject);
    }

    public void PlaySelectSound()
    {
        if (selectClip != null && _src != null)
            _src.PlayOneShot(selectClip, 1.0f);
    }
}
