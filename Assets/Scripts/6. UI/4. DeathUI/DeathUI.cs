using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject DeathMenu;

    public void Show(bool show)
    {
        DeathMenu.SetActive(show);
        Time.timeScale = 0f;
    }

    public void ReturnMain()
    {
        UIManager.Instance.uiController.SetBossHP(false);
        DeathMenu.SetActive(false);
        UIManager.Instance.uiController.pauseUI.OffSkillImages(UIManager.Instance.uiController.pauseUI.skillsCount);
        GameManager.Instance.SetStageInfo(0, 0, 1);
        Time.timeScale = 1f;
		SceneManager.LoadScene("StartScene");
    }
}
