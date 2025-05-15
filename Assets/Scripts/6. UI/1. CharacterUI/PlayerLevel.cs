using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : CharacterUI
{
    [SerializeField] private Text LevelUI;

    public override void UpdateValue(int current)
    {
        LevelUI.text = "Lv. " + current.ToString();
    }
}
