using UnityEngine;
using UnityEngine.UI;


/// 각 노드(UI Image + Button)에 붙여서 클릭을 MapManager에 전달.

[RequireComponent(typeof(Button))]

public class NodeController : MonoBehaviour
{
    #region NodeController 변수 선언
    [Header("노드별 크기 (Size Delta)")]
    public Vector2 enemySize = new Vector2(64, 64);
    public Vector2 healSize = new Vector2(80, 80);
    public Vector2 bossSize = new Vector2(120, 120);
    public Vector2 startSize = new Vector2(64, 64);
    public Vector2 eventSize = new Vector2(64, 64);

    private Vector3 originalScale;

    private int row, col;
    private NodeType type;
    private MapManager mapMgr;
    private Image img;

    #endregion

    //  노드의 행, 열 정보
    public int Row => row;
    public int Col => col;


    /// 맵매니저가 생성 직후 초기화

    #region Init 메서드 → 노드 초기화: 위치, 타입, 아이콘, 크기, 클릭 이벤트를 설정하는 기능
    public void Init(int r, int c, NodeType t, MapManager mgr, Sprite icon, byte alpha)
    {
        row = r;
        col = c;
        type = t;
        mapMgr = mgr;

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
            case NodeType.Start:
                rt.sizeDelta = startSize;
                break;
            case NodeType.Event:
                rt.sizeDelta = eventSize;
                break;
        }

        originalScale = rt.localScale;
    }

    #endregion

    #region OnClick 메서드 → 노드 클릭 시 MapManager에 클릭 이벤트를 전달하는 기능
    private void OnClick()
    {
        mapMgr.OnNodeClicked(row, col, type);
    }

    #endregion

    #region SetAlpha, SetHighlight 메서드 → 노드의 투명도(Alpha)를 설정하는 기능 / 노드 강조 여부에 따라 하이라이트를 설정하는 기능
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

    #endregion
}