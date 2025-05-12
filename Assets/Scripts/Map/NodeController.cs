using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NodeController : MonoBehaviour
{
    [Header("노드별 크기")]
    public Vector2 enemySize = new Vector2(64, 64);
    public Vector2 healSize = new Vector2(80, 80);
    public Vector2 bossSize = new Vector2(120, 120);

    int row, col;
    NodeType type;
    MapManager mapMgr;
    Image img;

    public int Row => row;
    public int Col => col;

    public void Init(int r, int c, NodeType t, MapManager mgr, Sprite icon, byte alpha)
    {
        row = r;
        col = c;
        type = t;
        mapMgr = mgr;

        img = GetComponent<Image>();
        img.sprite = icon;
        var color = img.color;
        color.a = alpha / 255f;
        img.color = color;

        var btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => mapMgr.OnNodeClicked(row, col, type));

        var rt = GetComponent<RectTransform>();
        switch (type)
        {
            case NodeType.Enemy: rt.sizeDelta = enemySize; break;
            case NodeType.Heal: rt.sizeDelta = healSize; break;
            case NodeType.Boss: rt.sizeDelta = bossSize; break;
        }
    }

    public void SetAlpha(byte alpha)
    {
        var c = img.color;
        c.a = alpha / 255f;
        img.color = c;
    }
}
