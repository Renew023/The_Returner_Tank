using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : BaseBattleUI
{
    [SerializeField] private Image hpBarFill;

    public override void UpdateValue(float current, float max)
    {
        hpBarFill.fillAmount = current / max;
    }
}
