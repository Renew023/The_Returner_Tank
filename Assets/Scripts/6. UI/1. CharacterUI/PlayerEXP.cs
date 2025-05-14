using UnityEngine;
using UnityEngine.UI;

public class PlayerEXP : CharacterUI
{
    [SerializeField] private Image expBarFill;

    public override void UpdateValue(float current, float max)
    {
        expBarFill.fillAmount = current / max;
    }
}
