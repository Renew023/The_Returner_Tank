using UnityEngine;
using UnityEngine.UI;

public class BossHP : CharacterUI
{
    [SerializeField] private Image hpBarFill;

    public override void UpdateValue(float current, float max)
    {
        hpBarFill.fillAmount = current / max;
    }
}
