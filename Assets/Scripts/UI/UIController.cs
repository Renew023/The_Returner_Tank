using UnityEngine;

public class UIController : MonoBehaviour
{
    public BossHP bossHP;
    public PlayerEXP playerEXP;
    public PlayerHP playerHP;
    public PlayerLevel playerLevel;
    public DeathUI deathUI;
    public PauseUI pauseUI;

    private void Start()
    {
        UIManager.Instance.uiController = this;
    }

    private void ShowBossHP(bool show)
    {
        bossHP.gameObject.SetActive(show);
    }

    private void ShowPlayerStatus(bool show)
    {
        // EXP, Level�� ���� Object�̹Ƿ� �� ���� �־ ��.
        playerHP.gameObject.SetActive(show);
    }

    private void ShowDeathUI(bool show)
    {
        deathUI.gameObject.SetActive(show);
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
    }

    public void SetBossHP(bool show)
    {
        ShowBossHP(show);
    }
}
