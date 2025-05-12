using UnityEngine;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject stageSelectMenu;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Show(bool show)
    {
        stageSelectMenu.SetActive(show);
    }
}
