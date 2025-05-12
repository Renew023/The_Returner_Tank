using UnityEngine;
using UnityEngine.UI;

public class BossHP : CharacterUI
{
    [SerializeField] private Image hpBarFill;

    protected override void Start()
    {
        // ������ �ִ� HP�� �����ͼ� �ݿ�
        //UpdateValue(�ִ� ü��, �ִ� ü��);
    }

    public override void UpdateValue(float current, float max)
    {
        hpBarFill.fillAmount = current / max;
    }
}
