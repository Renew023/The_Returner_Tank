using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// 각 노드(UI Image + Button)에 붙여서 클릭을 MapManager에 전달.

[RequireComponent(typeof(Button))]

public class NodeController : MonoBehaviour, IPointerClickHandler
{
    [Header("노드별 크기 (Size Delta)")]
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



    /// 맵매니저가 생성 직후 초기화

    public void Init(int r, int c, NodeType type, MapManager mgr, Sprite icon, float alpha)
    {
        Row = r;
        Col = c;
        Type = type;
        mapManager = mgr;

        mapManager = FindObjectOfType<MapManager>();

        // 아이콘 교체 & 기본 투명도 설정
        img = GetComponent<Image>();
        img.sprite = icon;
        var ccol = img.color;
        ccol.a = alpha / 255f;
        img.color = ccol;

        // 클릭 콜백
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

    /// 외부에서 투명도만 바꿀 때
    public void SetAlpha(byte alpha)
    {
        var c = img.color;
        c.a = alpha / 255f;
        img.color = c;
    }

    /// 외부에서 현재 선택 위치 표시용
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