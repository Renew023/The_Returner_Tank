using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    static SceneController()
    {
        // 씬 로드 후 호출될 메서드 연결
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void ToHeal() => SceneManager.LoadScene("Event_HealScene");
    public static void ToMap()  => SceneManager.LoadScene("MapScene");

    public static void ToBattle() => SceneManager.LoadScene("DungeonScene");

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MapScene" && MapManager.Instance != null)
        {
            // 맵씬으로 돌아왔을 때 위치 복원
            MapManager.Instance.RestoreMap();
        }
    }
}
