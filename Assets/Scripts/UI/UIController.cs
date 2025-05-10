using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] public BossHP bossHP;
    [SerializeField] public PlayerEXP playerEXP;
    [SerializeField] public PlayerHP playerHP;
    [SerializeField] public PlayerLevel playerLevel;
    [SerializeField] public DeathUI deathUI;
    [SerializeField] public PauseUI pauseUI;
    //[SerializeField] private StageSelectUI stageSelectUI;
    //[SerializeField] private SkillSelectUI skillSelectUI;

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
        // EXP, Level도 같은 Object이므로 한 개만 있어도 됨.
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
}
