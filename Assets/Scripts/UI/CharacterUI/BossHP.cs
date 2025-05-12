using UnityEngine;
using UnityEngine.UI;

public class BossHP : CharacterUI
{
    [SerializeField] private Image hpBarFill;

    protected override void Start()
    {
        // 보스의 최대 HP를 가져와서 반영
        //UpdateValue(최대 체력, 최대 체력);
    }

    public override void UpdateValue(float current, float max)
    {
        hpBarFill.fillAmount = current / max;
    }
}
