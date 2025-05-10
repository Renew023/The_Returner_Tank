using UnityEngine;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject DeathMenu;

    public void Show(bool show)
    {
        DeathMenu.SetActive(show);
    }
}
