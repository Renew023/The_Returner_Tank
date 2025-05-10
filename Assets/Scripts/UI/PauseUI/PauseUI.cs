using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

    private void Start()
    {
        UIManager.Instance.pauseUI = this;
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
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
    }
}
