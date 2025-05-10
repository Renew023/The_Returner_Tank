using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : CharacterUI
{
    [SerializeField] private Text LevelUI;

    protected override void Start()
    {
        UIManager.Instance.playerLevel = this;
        //UpdateValue(�÷��̾��� ����);
    }

    public override void UpdateValue(int current)
    {
        LevelUI.text = "Lv. " + current.ToString();
    }

    public override void Show(bool show)
    {
        //throw new System.NotImplementedException();
        LevelUI.gameObject.SetActive(show);
    }
}
