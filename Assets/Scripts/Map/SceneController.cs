using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    static SceneController()
    {
        // �� �ε� �� ȣ��� �޼��� ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void ToHeal() => SceneManager.LoadScene("Event_HealScene");
    public static void ToMap()  => SceneManager.LoadScene("MapScene");

	public static void ToBattle()
	{
		// 1 ~ 4 �� ���� ���� ����
		int randomIndex = Random.Range(1, 5); // ���Ѱ��� ���ܵǹǷ� 5�� �����ؾ� 4���� ���Ե�
		string sceneName = $"DungeonScene{randomIndex}";

		Debug.Log($"[SceneController] �ε��� ���� ��: {sceneName}");
		SceneManager.LoadScene(sceneName);
	}

	private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MapScene" && MapManager.Instance != null)
        {
            // �ʾ����� ���ƿ��� �� ��ġ ����
            MapManager.Instance.RestoreMap();
        }
    }
}
