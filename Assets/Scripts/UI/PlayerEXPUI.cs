using UnityEngine;
using UnityEngine.UI;

public class PlayerEXPUI : BaseBattleUI
{
    [SerializeField] private Image expBarFill;

    public override void UpdateValue(float current, float max)
    {
        expBarFill.fillAmount = current / max;
    }
}
