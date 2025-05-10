using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public BossHP bossHP;
    public PlayerEXP playerEXP;
    public PlayerHP playerHP;
    public PlayerLevel playerLevel;
    //public  pauseUI;
    //public  deathUI;
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

    //public void PauseGame()
    //{
    //    Time.timeScale = 0;
    //    pauseUI.SetActive(true);
    //}

    //public void ResumeGame()
    //{
    //    Time.timeScale = 1;
    //    pauseUI.SetActive(false);
    //}
}
