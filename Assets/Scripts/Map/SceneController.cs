using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    private static string _lastSceneName;
    private const float MapReturnYOffset = 2f;

    static SceneController()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void ToHeal()
    {
        _lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Event_HealScene");
    }

    public static void ToBattle()
    {
        _lastSceneName = SceneManager.GetActiveScene().name;

        int randomIndex = Random.Range(1, 5);
        string sceneName = $"DungeonScene{randomIndex}";
        Debug.Log($"[SceneController] 로딩할 전투 씬: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public static void ToMap()
    {
        _lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("MapScene");
    }

    public static void ToBoss()
    {
        Debug.Log($"[SceneController] 로딩할 전투 씬: 보스 씬");
        SceneManager.LoadScene("BossBattleScene");
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MapScene")
            return;

        if (MapManager.Instance != null)
            MapManager.Instance.RestoreMap();

        if (_lastSceneName.StartsWith("DungeonScene")
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