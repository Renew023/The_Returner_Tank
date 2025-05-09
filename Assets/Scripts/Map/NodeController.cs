using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 각 노드(UI Image + Button)에 붙여서 클릭을 MapManager에 전달.
/// </summary>
[RequireComponent(typeof(Button))]
public class NodeController : MonoBehaviour
{
    private int row, col;
    private NodeType type;
    private MapManager mapMgr;
    private Image img;

    /// <summary>
    /// 맵매니저가 생성 직후 초기화
    /// </summary>
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
    }

    private void OnClick()
    {
        mapMgr.OnNodeClicked(row, col, type);
    }

    /// <summary>외부에서 투명도만 바꿀 때</summary>
    public void SetAlpha(byte alpha)
    {
        var c = img.color;
        c.a = alpha / 255f;
        img.color = c;
    }

    /// <summary>외부에서 현재 선택 위치 표시용</summary>
    public void SetHighlight(bool on)
    {
        img.color = on
            ? new Color(img.color.r, img.color.g, img.color.b, 1f)
            : new Color(img.color.r, img.color.g, img.color.b, img.color.a);
    }
}