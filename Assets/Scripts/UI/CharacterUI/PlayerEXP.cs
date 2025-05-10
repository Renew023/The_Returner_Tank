using UnityEngine;
using UnityEngine.UI;

public class PlayerEXP : CharacterUI
{
    [SerializeField] private Image expBarFill;

    protected override void Start()
    {
        UIManager.Instance.playerEXP = this;
        // �÷��̾��� ���� ����ġ�� �����ͼ� �ݿ�
        //UpdateValue(���� ����ġ, �� �ʿ� ����ġ);
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
