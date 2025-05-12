using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] public BossHP bossHP;
    [SerializeField] public PlayerEXP playerEXP;
    [SerializeField] public PlayerHP playerHP;
    [SerializeField] public PlayerLevel playerLevel;
    [SerializeField] public DeathUI deathUI;
    [SerializeField] public PauseUI pauseUI;
    //[SerializeField] public StageSelectUI stageSelectUI;
    //[SerializeField] public SkillSelectUI skillSelectUI;

    private void Start()
    {
        UIManager.Instance.uiController = this;
    }

    public void ShowBossHP(bool show)
    {
        bossHP.gameObject.SetActive(show);
    }

    public void ShowPlayerStatus(bool show)
    {
        // EXP, Level�� ���� Object�̹Ƿ� �� ���� �־ ��.
        playerHP.gameObject.SetActive(show);
    }

    public void ShowDeathUI(bool show)
    {
        deathUI.gameObject.SetActive(show);
    }

    public void ShowPauseUI(bool show)
    {
        deathUI.gameObject.SetActive(show);
    }

    //public void ShowStageSelectUI(bool show)
    //{
    //    stageSelectUI.gameObject.SetActive(show);
    //}

    //public void ShowSkillSelectUI(bool show)
    //{
    //    skillSelectUI.gameObject.SetActive(show);
    //}
}
