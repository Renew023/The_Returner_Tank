using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : CharacterUI
{
    [SerializeField] private Image hpBarFill;

    protected override void Start()
    {
        // 플레이어의 현제 체력을 가져와서 반영
        //UpdateValue(현재 체력, 최대 체력);
    }

    public override void UpdateValue(float current, float max)
    {
        hpBarFill.fillAmount = current / max;
    }
}
