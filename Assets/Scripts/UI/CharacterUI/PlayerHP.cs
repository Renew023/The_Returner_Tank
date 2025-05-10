using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : CharacterUI
{
    [SerializeField] private Image hpBarFill;

    protected override void Start()
    {
        UIManager.Instance.playerHP = this;
        // �÷��̾��� ���� ü���� �����ͼ� �ݿ�
        //UpdateValue(���� ü��, �ִ� ü��);
    }

    public override void UpdateValue(float current, float max)
    {
        hpBarFill.fillAmount = current / max;
    }

    public override void Show(bool show)
    {
        hpBarFill.gameObject.SetActive(show);
    }
}
