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

    public void OffSkillImages(int _skillsCount)
    {
        for (int i = 0; i < _skillsCount; ++i)
        {
            skillSlots[i].color = new Color(255, 255, 255, 0);
            skillSlots[i].sprite = null;
        }

        skillsCount = 0;
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
        PauseMenu.SetActive(false);
        OffSkillImages(skillsCount);
        GameManager.Instance.SetStageInfo(0, 0, 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}
