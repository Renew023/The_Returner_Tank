using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    #region PauseUI 객체 변수 선언
    [SerializeField] private GameObject PauseMenu;
    public Image[] skillSlots;

    public int skillsCount = 0;

    #endregion

    #region SetSkillImages, OffSkillImages 메서드
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

    #endregion

    #region PauseMenuToggle 메서드
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

    #endregion

    #region ReturnMain 메서드
    public void ReturnMain()
    {
        UIManager.Instance.uiController.SetBossHP(false);
        PauseMenu.SetActive(false);
        OffSkillImages(skillsCount);
        GameManager.Instance.SetStageInfo(0, 0, 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    #endregion
}