using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] public BossHP bossHP;
    [SerializeField] public PlayerEXP playerEXP;
    [SerializeField] public PlayerHP playerHP;
    [SerializeField] public PlayerLevel playerLevel;
    [SerializeField] public DeathUI deathUI;
    [SerializeField] public PauseUI pauseUI;

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
        // EXP, Level도 같은 Object이므로 한 개만 있어도 됨.
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
