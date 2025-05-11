using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

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
        Time.timeScale = 1f;
        //SceneManager.LoadScene("StartScene");
        Debug.Log("씬을 추가 해주세요.");
    }
}
