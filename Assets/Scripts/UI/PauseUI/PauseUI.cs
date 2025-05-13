using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    public Image[] skillSlots;

    public int skillsCount = 0;

    public void SetSkillImages(int _skillsCount)
    {
        skillSlots[_skillsCount].color = new Color(255, 255, 255, 255);
        skillsCount++;
    }

    public void PauseMenuToggle()
    {
        if (PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
        }
    }

    public void ReturnMain()
    {
        Time.timeScale = 1f;
        UIManager.Instance.uiController.SetDungeonUI(false);
        SceneManager.LoadScene("StartScene");
    }
}
