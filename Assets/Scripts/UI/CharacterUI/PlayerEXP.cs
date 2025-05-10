using UnityEngine;
using UnityEngine.UI;

public class PlayerEXP : CharacterUI
{
    [SerializeField] private Image expBarFill;

    protected override void Start()
    {
        UIManager.Instance.playerEXP = this;
        // 플레이어의 현재 경험치를 가져와서 반영
        //UpdateValue(현재 경험치, 총 필요 경험치);
    }

    public override void UpdateValue(float current, float max)
    {
        expBarFill.fillAmount = current / max;
    }

    public override void Show(bool show)
    {
        expBarFill.gameObject.SetActive(show);
    }
}
