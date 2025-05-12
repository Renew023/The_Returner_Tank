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

	public static void ToBattle()
	{
		// 1 ~ 4 중 랜덤 숫자 선택
		int randomIndex = Random.Range(1, 5); // 상한값은 제외되므로 5로 설정해야 4까지 포함됨
		string sceneName = $"DungeonScene{randomIndex}";

		Debug.Log($"[SceneController] 로딩할 전투 씬: {sceneName}");
		SceneManager.LoadScene(sceneName);
	}

	private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MapScene" && MapManager.Instance != null)
        {
            // 맵씬으로 돌아왔을 때 위치 복원
            MapManager.Instance.RestoreMap();
        }
    }
}
