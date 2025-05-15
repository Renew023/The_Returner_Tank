using UnityEngine;

public class UIController : MonoBehaviour
{
    #region UIController 객체 변수 선언
    public BossHP bossHP;
    public PlayerEXP playerEXP;
    public PlayerHP playerHP;
    public PlayerLevel playerLevel;
    public DeathUI deathUI;
    public ClearUI clearUI;
    public PauseUI pauseUI;

    #endregion

    #region ShowBossHP 메서드
    private void ShowBossHP(bool show)
    {
        bossHP.gameObject.SetActive(show);
    }

    #endregion

    #region ShowPlayerStatusm 메서드
    private void ShowPlayerStatus(bool show)
    {
        // EXP, Level도 같은 Object이므로 한 개만 있어도 됨.
        playerHP.gameObject.SetActive(show);
    }

    #endregion

    #region ShowDeathUI, ShowClearUI, ShowPauseUI, SetDungeonUI 메서드
    private void ShowDeathUI(bool show) 
    {
        deathUI.gameObject.SetActive(show);
    }

    private void ShowClearUI(bool show)
    {
        clearUI.gameObject.SetActive(show);
    }

    private void ShowPauseUI(bool show)
    {
        pauseUI.gameObject.SetActive(show);
    }

    public void SetDungeonUI(bool show)
    {
        ShowPlayerStatus(show);
        ShowDeathUI(show);
        ShowPauseUI(show);
        ShowClearUI(show);
    }

    #endregion

    #region SetBossHP 메서드
    public void SetBossHP(bool show)
    {
        ShowBossHP(show);
    }

    #endregion
}