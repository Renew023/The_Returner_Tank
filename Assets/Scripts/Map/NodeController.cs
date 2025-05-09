// Assets/Scripts/NodeController.cs
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    private int row, col;
    private NodeType type;
    private MapManager mapMgr;

    public void Init(int r, int c, NodeType t, MapManager mgr)
    {
        row = r; col = c;
        type = t;
        mapMgr = mgr;

        // 클릭 리스너
        GetComponent<Button>().onClick.AddListener(() =>
            mapMgr.OnNodeClicked(row, col, type)
        );
    }
}
