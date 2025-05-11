using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject DeathMenu;

    public void Show(bool show)
    {
        DeathMenu.SetActive(show);
    }

    public void ReturnMain()
    {
        //SceneManager.LoadScene("StartScene");
        Debug.Log("���� �߰� ���ּ���.");
    }
}
