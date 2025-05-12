using UnityEngine;
using UnityEngine.UI;

public class BossHP : CharacterUI
{
    [SerializeField] private Image hpBarFill;

    protected override void Start()
    {
        UIManager.Instance.bossHP = this;
        // ������ �ִ� HP�� �����ͼ� �ݿ�
        //UpdateValue(�ִ� ü��, �ִ� ü��);
    }

    public override void UpdateValue(float current, float max)
    {
        hpBarFill.fillAmount = current / max;
    }

    public override void Show(bool show)
    {
        //throw new System.NotImplementedException();
        hpBarFill.gameObject.SetActive(show);
    }
}
