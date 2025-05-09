using UnityEngine;
using UnityEngine.UI;

public class PlayerEXPUI : BaseBattleUI
{
    [SerializeField] private Image expBarFill;

    public void UpdateEXP(float current, float max)
    {
        expBarFill.fillAmount = current / max;
    }
}
