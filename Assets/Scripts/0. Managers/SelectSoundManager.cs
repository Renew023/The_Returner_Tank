using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SelectSoundManager : MonoBehaviour
{
    #region SelectSoundManager 객체 변수 선언
    public static SelectSoundManager Instance { get; private set; }

    [Header("Select SFX")]
    public AudioClip selectClip;    // Inspector에 드래그할 효과음 파일

    AudioSource _src;

    #endregion

    #region Awake 메서드 
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

    #endregion

    #region PlaySelectSound 메서드 → 선택 효과음을 1회 재생하는 기능
    public void PlaySelectSound()
    {
        if (selectClip != null && _src != null)
            _src.PlayOneShot(selectClip, 1.0f);
    }

    #endregion
}
