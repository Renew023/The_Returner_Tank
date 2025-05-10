
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void ToMap() => SceneManager.LoadScene("MapScene");
    //public static void ToBattle() => SceneManager.LoadScene("DungeonScene1");

    public static void ToBattle()
    {
        int randonIndex = UnityEngine.Random.Range(1, 5);
        string sceneName = $"DungeonScene{randonIndex}";

        Debug.Log($"[SceneController] 로딩할 전투 씬: {sceneName}");
        SceneManager.LoadScene(sceneName);

    }

    public static void ToHeal() => SceneManager.LoadScene("HealScene");
}
