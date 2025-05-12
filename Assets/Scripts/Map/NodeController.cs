using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// �� ���(UI Image + Button)�� �ٿ��� Ŭ���� MapManager�� ����.

[RequireComponent(typeof(Button))]

public class NodeController : MonoBehaviour, IPointerClickHandler
{
    [Header("��庰 ũ�� (Size Delta)")]
    public Vector2 enemySize = new Vector2(64, 64);
    public Vector2 healSize = new Vector2(80, 80);
    public Vector2 bossSize = new Vector2(120, 120);

    private Vector3 originalScale;

    private int row, col;
    private MapManager mapManager;
    private NodeType type;
    private MapManager mapMgr;
    private Image img;

    public int Row => row;
    public int Col => col;



    /// �ʸŴ����� ���� ���� �ʱ�ȭ

    public void Init(int r, int c, NodeType type, MapManager mgr, Sprite icon, float alpha)
    {
        Row = r;
        Col = c;
        Type = type;
        mapManager = mgr;

        mapManager = FindObjectOfType<MapManager>();

        // ������ ��ü & �⺻ ���� ����
        img = GetComponent<Image>();
        img.sprite = icon;
        var ccol = img.color;
        ccol.a = alpha / 255f;
        img.color = ccol;

        // Ŭ�� �ݹ�
        GetComponent<Button>().onClick.AddListener(OnClick);

        var rt = GetComponent<RectTransform>();
        switch (t)
        {
            case NodeType.Enemy:
                rt.sizeDelta = enemySize;
                break;
            case NodeType.Heal:
                rt.sizeDelta = healSize;
                break;
            case NodeType.Boss:
                rt.sizeDelta = bossSize;
                break;
        }

        originalScale = rt.localScale;
    }

    private void OnClick()
    {
        mapMgr.OnNodeClicked(row, col, type);
    }

    /// �ܺο��� ������ �ٲ� ��
    public void SetAlpha(byte alpha)
    {
        var c = img.color;
        c.a = alpha / 255f;
        img.color = c;
    }

    /// �ܺο��� ���� ���� ��ġ ǥ�ÿ�
    public void SetHighlight(bool on)
    {
        img.color = on
            ? new Color(img.color.r, img.color.g, img.color.b, 1f)
            : new Color(img.color.r, img.color.g, img.color.b, img.color.a);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mapManager.OnNodeSelected(Row, Col, Type);
    }
}