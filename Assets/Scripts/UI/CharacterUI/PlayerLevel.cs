using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : CharacterUI
{
    [SerializeField] private Text LevelUI;

    protected override void Start()
    {
        //UpdateValue(�÷��̾��� ����);
    }

    public override void UpdateValue(int current)
    {
        LevelUI.text = "Lv. " + current.ToString();
    }
}
