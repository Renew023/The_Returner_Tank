using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour
{
    [SerializeField] private GameObject ClearMenu;

    public void Show(bool show)
    {
        ClearMenu.SetActive(show);
        Time.timeScale = 0f;
    }

    public void ReturnMain()
    {
        UIManager.Instance.uiController.SetDungeonUI(false);
        GameManager.Instance.SetStageInfo(0, 0, 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}
