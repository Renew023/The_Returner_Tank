using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : BaseBattleUI
{
    [SerializeField] private Image hpBarFill;

    public void UpdateHP(float current, float max)
    {
        hpBarFill.fillAmount = current / max;
    }
}
