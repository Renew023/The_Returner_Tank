// Assets/Scripts/SceneController.cs
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void GoToMap()
    {
        SceneManager.LoadScene("MapScene");
    }

    public static void GoToBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public static void GoToHeal()
    {
        SceneManager.LoadScene("HealScene");
    }
}
