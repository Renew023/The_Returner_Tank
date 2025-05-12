using UnityEngine;
using UnityEngine.UI;

public class PlayerEXP : CharacterUI
{
    [SerializeField] private Image expBarFill;

    protected override void Start()
    {
        // �÷��̾��� ���� ����ġ�� �����ͼ� �ݿ�
        //UpdateValue(���� ����ġ, �� �ʿ� ����ġ);
    }

    public override void UpdateValue(float current, float max)
    {
        expBarFill.fillAmount = current / max;
    }
}
