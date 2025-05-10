
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    private RectTransform playerIndicatorRt;

    [Header("Prefabs & Icons")]
    public NodeController nodePrefab;
    public Image dotPrefab;
    public Sprite enemyIcon, healIcon, bossIcon;
    public GameObject playerIndicatorPrefab;

    [Header("레이아웃")]
    public float baseYOffset = -50f;
    public float extraYOffset = 50f;
    public RectTransform stageContainer;
    public int totalRows = 4;
    public int choicesPerRow = 3;
    public float xSpacing = 100f, ySpacing = -100f;
    public byte defaultAlpha = 160, activeAlpha = 255;
    public int dotSegments = 8;

    
    static bool initialized = false;
    static List<List<NodeType>> mapData;
    List<List<NodeController>> mapNodes;
    List<Image> dotLines;
    int currentRow = 0, currentCol = 0;

    void Start()
    {
        if (!initialized)
        {
            GenerateMapData();
            initialized = true;
        }
        RenderMap();
    }

    void GenerateMapData()
    {
        mapData = new List<List<NodeType>>();
        var rnd = new System.Random();

        for (int r = 0; r < totalRows - 1; r++)
        {
            var rowList = new List<NodeType>();
            for (int i = 0; i < choicesPerRow; i++)
                rowList.Add(rnd.Next(10) < 8 ? NodeType.Enemy : NodeType.Heal);
            mapData.Add(rowList);
        }
        mapData.Add(new List<NodeType> { NodeType.Boss });
    }

    void RenderMap()
    {
        // 기존 노드/점선 제거
        if (mapNodes != null) foreach (var row in mapNodes) foreach (var n in row) Destroy(n.gameObject);
        if (dotLines != null) foreach (var d in dotLines) Destroy(d.gameObject);

        mapNodes = new List<List<NodeController>>();
        dotLines = new List<Image>();

        // 1) 노드 생성
        for (int r = 0; r < mapData.Count; r++)
        {
            var rowList = new List<NodeController>();
            int count = mapData[r].Count;
            for (int c = 0; c < count; c++)
            {
                var nc = Instantiate(nodePrefab, stageContainer);
                Sprite icon = mapData[r][c] == NodeType.Enemy ? enemyIcon
                             : mapData[r][c] == NodeType.Heal ? healIcon
                                                               : bossIcon;
                nc.Init(r, c, mapData[r][c], this, icon, defaultAlpha);

                var rt = nc.GetComponent<RectTransform>();
                float y = r * ySpacing + baseYOffset;
                float x = (c - (count - 1) / 2f) * xSpacing;
                rt.anchoredPosition = new Vector2(x, y);


                rowList.Add(nc);
            }
            mapNodes.Add(rowList);
        }

        // 2) 점선 연결
        for (int r = 0; r < mapNodes.Count - 1; r++)
        {
            var curr = mapNodes[r];
            var next = mapNodes[r + 1];
            for (int i = 0; i < curr.Count && i < next.Count; i++)
                DrawDots(curr[i], next[i]);
        }
        var bossCtrl = mapNodes[mapNodes.Count - 1][0];
        foreach (var prev in mapNodes[mapNodes.Count - 2])
            DrawDots(prev, bossCtrl);


        // 3) 플레이어 표시
        var pi = Instantiate(playerIndicatorPrefab, stageContainer);
        playerIndicatorRt = pi.GetComponent<RectTransform>();
        pi.GetComponent<RectTransform>().anchoredPosition =
            mapNodes[0][0].GetComponent<RectTransform>().anchoredPosition;

        // 4) 활성 행 투명도
        UpdateAlphas();
    }

    void DrawDots(NodeController a, NodeController b)
    {
        var pa = a.GetComponent<RectTransform>().anchoredPosition;
        var pb = b.GetComponent<RectTransform>().anchoredPosition;
        for (int i = 1; i < dotSegments; i++)
        {
            float t = i / (float)dotSegments;
            var pos = Vector2.Lerp(pa, pb, t);
            var dot = Instantiate(dotPrefab, stageContainer);
            dot.rectTransform.anchoredPosition = pos;
            dotLines.Add(dot);
        }
    }

    void UpdateAlphas()
    {
        for (int r = 0; r < mapNodes.Count; r++)
            for (int c = 0; c < mapNodes[r].Count; c++)
                mapNodes[r][c].SetAlpha((r == currentRow) ? activeAlpha : defaultAlpha);
    }

    // 클릭 시 호출
    public void OnNodeClicked(int r, int c, NodeType t)
    {
        currentRow = r; currentCol = c;
        mapNodes[r][c].SetAlpha(activeAlpha);

        var pi = stageContainer
                 .GetComponentInChildren<Image>(true); 
        pi.GetComponent<RectTransform>().anchoredPosition =
            mapNodes[r][c].GetComponent<RectTransform>().anchoredPosition;

        if (playerIndicatorRt != null)
            playerIndicatorRt.anchoredPosition =
                mapNodes[r][c].GetComponent<RectTransform>().anchoredPosition;

        // 씬 전환
        if (t == NodeType.Enemy || t == NodeType.Boss) SceneController.ToBattle();
        else SceneController.ToHeal();
    }
}
