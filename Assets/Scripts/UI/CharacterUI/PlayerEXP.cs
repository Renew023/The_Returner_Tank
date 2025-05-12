using UnityEngine;
using UnityEngine.UI;

public class PlayerEXP : CharacterUI
{
    [SerializeField] private Image expBarFill;

    protected override void Start()
    {
        // 플레이어의 현재 경험치를 가져와서 반영
        //UpdateValue(현재 경험치, 총 필요 경험치);
    }

    public override void UpdateValue(float current, float max)
    {
        expBarFill.fillAmount = current / max;
    }
}
