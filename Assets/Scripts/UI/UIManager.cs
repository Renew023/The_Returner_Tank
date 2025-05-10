using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public BossHP bossHP;
    public PlayerEXP playerEXP;
    public PlayerHP playerHP;
    public PlayerLevel playerLevel;
    public PauseUI pauseUI;
    public DeathUI deathUI;
    //public  stageSelectUI;
    //public  skillSelectUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
