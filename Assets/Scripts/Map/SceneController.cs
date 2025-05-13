using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    private static string _lastSceneName;
    private const float MapReturnYOffset = 2f;

    static SceneController()
    {
        // 씬 로드 콜백 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // --- Heal Event Scene 진입 시힐 스폰 ---
        if (scene.name == "Event_HealScene")
        {
            var HealPrefab = Resources.Load<GameObject>("HealPrefab");
            if (HealPrefab != null)
            {
                // 월드 좌표 (0,5,0)에 스폰
                UnityEngine.Object.Instantiate(
                    HealPrefab,
                    new Vector3(0f, 5f, 0f),
                    Quaternion.identity
                );
            }
        }

        // --- Map Scene 복귀 처리 ---
        if (scene.name == "MapScene")
        {
            if (MapManager.Instance != null)
                MapManager.Instance.RestoreMap();

            // 던전/힐 → 맵 복귀 시 Y 오프셋 적용
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