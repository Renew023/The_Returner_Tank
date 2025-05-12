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
        SceneManager.LoadScene("Event_HealScene", LoadSceneMode.Single);
    }

    public static void ToBattle()
    {
        _lastSceneName = SceneManager.GetActiveScene().name;
        int randomIndex = Random.Range(1, 5);
        SceneManager.LoadScene($"DungeonScene{randomIndex}", LoadSceneMode.Single);
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

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Event_HealScene")
        {
            var healPrefab = Resources.Load<GameObject>("HealPrefab");
            if (healPrefab != null)
                Object.Instantiate(healPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity);
        }

        if (scene.name == "MapScene")
        {
            MapManager.Instance?.RestoreMap();

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
