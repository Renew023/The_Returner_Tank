using UnityEngine;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject DeathMenu;

    private void Start()
    {
        UIManager.Instance.deathUI = this;
    }

    public void Show(bool show)
    {
        DeathMenu.SetActive(show);
    }
}
