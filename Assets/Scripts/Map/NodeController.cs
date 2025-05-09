using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �� ���(UI Image + Button)�� �ٿ��� Ŭ���� MapManager�� ����.
/// </summary>
[RequireComponent(typeof(Button))]
public class NodeController : MonoBehaviour
{
    private int row, col;
    private NodeType type;
    private MapManager mapMgr;
    private Image img;

    /// <summary>
    /// �ʸŴ����� ���� ���� �ʱ�ȭ
    /// </summary>
    public void Init(int r, int c, NodeType t, MapManager mgr, Sprite icon, byte alpha)
    {
        row = r;
        col = c;
        type = t;
        mapMgr = mgr;

        // ������ ��ü & �⺻ ���� ����
        img = GetComponent<Image>();
        img.sprite = icon;
        var ccol = img.color;
        ccol.a = alpha / 255f;
        img.color = ccol;

        // Ŭ�� �ݹ�
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        mapMgr.OnNodeClicked(row, col, type);
    }

    /// <summary>�ܺο��� ������ �ٲ� ��</summary>
    public void SetAlpha(byte alpha)
    {
        var c = img.color;
        c.a = alpha / 255f;
        img.color = c;
    }

    /// <summary>�ܺο��� ���� ���� ��ġ ǥ�ÿ�</summary>
    public void SetHighlight(bool on)
    {
        img.color = on
            ? new Color(img.color.r, img.color.g, img.color.b, 1f)
            : new Color(img.color.r, img.color.g, img.color.b, img.color.a);
    }
}