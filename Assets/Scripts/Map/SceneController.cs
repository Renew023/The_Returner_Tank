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

    public static void ToBattle() => SceneManager.LoadScene("DungeonScene");

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MapScene" && MapManager.Instance != null)
        {
            // �ʾ����� ���ƿ��� �� ��ġ ����
            MapManager.Instance.RestoreMap();
        }
    }
}
