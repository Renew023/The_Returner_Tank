using UnityEngine;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject skillSelectMenu;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Show(bool show)
    {
        if (show)
        {
            Time.timeScale = 0f;
            skillSelectMenu.SetActive(true);
        }
        else
        {
            skillSelectMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
