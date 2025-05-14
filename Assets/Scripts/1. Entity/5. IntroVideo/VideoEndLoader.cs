using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndLoader : MonoBehaviour
{
    public string sceneToLoad = "StartScene";  // 로드할 씬 이름

    private VideoPlayer vp;

    void Awake()
    {
        vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += OnVideoFinished;
    }

    void Start()
    {
        vp.Play();
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        // 씬 전환
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnDestroy()
    {
        // 콜백 해제
        vp.loopPointReached -= OnVideoFinished;
    }
}


