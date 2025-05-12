using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("BGM Clips")]
    public AudioClip startBgm;
    public AudioClip battleBgm;
    public AudioClip bossBgm;
    public AudioClip healBgm;

    AudioSource _src;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _src = GetComponent<AudioSource>();
            Play(startBgm);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MapScene")
        {
            Play(startBgm);
        }
        else if (scene.name.StartsWith("DungeonScene"))
        {
            Play(battleBgm);
        }
        else if (scene.name == "BossBattleScene")
        {
            Play(bossBgm);
        }
        else if (scene.name == "Event_HealScene")
        {
            Play(healBgm);
        }
    }

    void Play(AudioClip clip)
    {
        if (_src.clip == clip) return;
        _src.clip = clip;
        _src.loop = true;
        _src.Play();
    }
}
