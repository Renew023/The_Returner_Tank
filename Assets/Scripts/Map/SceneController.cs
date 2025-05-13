using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    private static string _lastSceneName;
    private const float MapReturnYOffset = 2f;

    static SceneController()
    {
        // �� �ε� �ݹ� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void ToHeal()
    {
        // ���� ���� �� �̸� ����
        _lastSceneName = SceneManager.GetActiveScene().name;
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

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // --- Heal Event Scene ���� ���� ���� ---
        if (scene.name == "Event_HealScene")
        {
            var HealPrefab = Resources.Load<GameObject>("HealPrefab");
            if (HealPrefab != null)
            {
                // ���� ��ǥ (0,5,0)�� ����
                UnityEngine.Object.Instantiate(
                    HealPrefab,
                    new Vector3(0f, 5f, 0f),
                    Quaternion.identity
                );
            }
        }

        // --- Map Scene ���� ó�� ---
        if (scene.name == "MapScene")
        {
            if (MapManager.Instance != null)
                MapManager.Instance.RestoreMap();

            // ����/�� �� �� ���� �� Y ������ ����
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