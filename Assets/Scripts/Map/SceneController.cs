
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void ToMap() => SceneManager.LoadScene("MapScene");
    public static void ToBattle() => SceneManager.LoadScene("BattleScene");
    public static void ToHeal() => SceneManager.LoadScene("HealScene");
}
