using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    #region SceneController 객체 변수 선언
    private static string _lastSceneName;
    private const float MapReturnYOffset = 2f;
    private static bool needResetMap = false;

    #endregion

    static SceneController()
    {
        // 씬 로드 콜백 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    #region ToHeal, ToBattle, ToBoss, ToMap, ToFirstMap, ToEvent 메서드 → MapScene에서 각 씬으로 전환하는 기능

    public static void ToHeal()
    {
        // 진입 직전 씬 이름 저장
        _lastSceneName = SceneManager.GetActiveScene().name;
        GameManager.Instance.IncreaseDungeonLevel();
        SceneManager.LoadScene("Event_HealScene", LoadSceneMode.Single);

    }

    public static void ToBattle()
    {
        _lastSceneName = SceneManager.GetActiveScene().name;

        int randomIndex = Random.Range(1, 5);
        string sceneName = $"DungeonScene{randomIndex}";
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public static void ToBoss()
    {
        _lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("BossBattleScene", LoadSceneMode.Single);
    }

    public static void ToMap()
    {
        _lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("MapScene", LoadSceneMode.Single);
    }

    public static void ToFirstMap()
    {
        needResetMap = true;
        SceneManager.LoadScene("MapScene");

    }

    public static void ToEvent()
    {
        _lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("EventScene", LoadSceneMode.Single);

    }

    #endregion

    #region OnSceneLoaded 메서드 → 씬이 로드되었을 때 호출되는 콜백 기능
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        // --- Map Scene 복귀 처리 ---
        if (scene.name == "MapScene")
        {
            if (needResetMap == true)
            {
                MapManager.Instance.ResetMap();
                needResetMap = false;
            }
            else
            {
                if (MapManager.Instance != null)
                    MapManager.Instance.RestoreMap();

                // 던전/힐 → 맵 복귀 시 카메라 설정
                if (_lastSceneName.StartsWith("DungeonScene")
                 || _lastSceneName == "BossBattleScene"
                 || _lastSceneName == "Event_HealScene")
                {
                    var cam = Camera.main;
                    if (cam != null)
                    {
                        var pos = cam.transform.position;
                        pos.y += MapReturnYOffset;
                        cam.transform.position = pos;
                    }
                }
            }

        }
    }

    #endregion
}