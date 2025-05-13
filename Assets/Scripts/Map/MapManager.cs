using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class MapManager : MonoBehaviour
{
    private HashSet<string> connections = new HashSet<string>();
    public static MapManager Instance { get; private set; }

    [Header("Prefabs & Icons")]
    public NodeController nodePrefab;
    public Image dotPrefab;
    public Sprite enemyIcon, healIcon, bossIcon, startIcon;
    public GameObject playerIndicatorPrefab;
    public GameObject startNodePrefab;  // Start 노드 프리팹

    [Header("Layout")]
    public RectTransform stageContainer;
    public int totalRows = 4;
    public int choicesPerRow = 3;
    public float xSpacing = 100f;
    public float ySpacing = -100f;
    public float baseYOffset = -50f;
    public byte defaultAlpha = 160;
    public byte activeAlpha = 255;
    public int dotSegments = 8;


    // 저장된 맵 데이터와 상태
    static bool initialized = false;
    static List<List<NodeType>> mapData;
    List<List<NodeController>> mapNodes;
    List<Image> dotLines;
    int currentRow = 0, currentCol = 0;

    RectTransform playerIndicatorRt;
    RectTransform startNodeRt;  // Start 노드 위치 저장

    private bool IsNodeReachable(int r, int c)
    {
        if (r <= currentRow) return false;
        string key = $"{currentRow},{currentCol}-{r},{c}";
        return connections.Contains(key);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MapScene")
            InitializeOrRestoreMap();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MapScene")
        {
            var go = GameObject.Find("Canvas/StageContainer");
            if (go != null)
                stageContainer = go.GetComponent<RectTransform>();
            else
                Debug.LogError("MapManager: 'Canvas/StageContainer'를 찾을 수 없습니다.");

            InitializeOrRestoreMap();
        }
    }

    void InitializeOrRestoreMap()
    {
        if (!initialized && GameManager.Instance.FirstMapEntry)
        {
            GenerateMapData();
            initialized = true;
            GameManager.Instance.DisableFirstMapEntry();
        }
        RenderMap();
        RestoreMap();
    }

    void GenerateMapData()
    {
        mapData = new List<List<NodeType>>();
        var rnd = new System.Random();
        // 첫재쭐 스타트 노드
        mapData.Add(new List<NodeType> { NodeType.Start });
        for (int r = 0; r < totalRows - 1; r++)
        {
            var rowList = new List<NodeType>();
            for (int i = 0; i < choicesPerRow; i++)
                rowList.Add(rnd.Next(10) < 8 ? NodeType.Enemy : NodeType.Heal);
            mapData.Add(rowList);
        }
        // 마지막 줄은 보스 한 마리
        mapData.Add(new List<NodeType> { NodeType.Boss });
    }

    void RenderMap()
    {
        // 기존 노드/점선 삭제
        if (mapNodes != null)
        {
            foreach (var row in mapNodes)
                foreach (var n in row)
                    if (n != null && n.gameObject != null)
                        Destroy(n.gameObject);
        }
        if (dotLines != null)
        {
            foreach (var d in dotLines)
                if (d != null && d.gameObject != null)
                    Destroy(d.gameObject);
        }

        connections.Clear();
        mapNodes = new List<List<NodeController>>();
        dotLines = new List<Image>();
        startNodeRt = null;

        // 노드 배치
        for (int r = 0; r < mapData.Count; r++)
        {
            var rowList = new List<NodeController>();
            int count = mapData[r].Count;
            for (int c = 0; c < count; c++)
            {
                var nc = Instantiate(nodePrefab, stageContainer);
                // 크기 조정: Start 노드만 작게 표시
                if (mapData[r][c] == NodeType.Start)
                {
                    var rtStart = nc.GetComponent<RectTransform>();
                    // 원하는 크기로 설정 (예: 가로 50, 세로 50)
                    rtStart.sizeDelta = new Vector2(1f, 1f);
                    // 추가: 스케일 조정 시 필요할 경우
                    nc.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                    startNodeRt = rtStart;
                }
                // 아이콘 선택
                Sprite icon = mapData[r][c] switch
                {
                    NodeType.Enemy => enemyIcon,
                    NodeType.Heal => healIcon,
                    NodeType.Start => startIcon,
                    _ => bossIcon
                };
                nc.Init(r, c, mapData[r][c], this, icon, defaultAlpha);

                // 위치 계산
                float y = r * ySpacing + baseYOffset;
                float x = (c - (count - 1) / 2f) * xSpacing;
                nc.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

                rowList.Add(nc);
            }
            mapNodes.Add(rowList);
        }


        // 점선 연결 로직 유지
        for (int r = 0; r < mapNodes.Count - 1; r++)
        {
            var curr = mapNodes[r];
            var next = mapNodes[r + 1];
            for (int i = 0; i < curr.Count && i < next.Count; i++)
                DrawDots(curr[i], next[i]);
        }
        // start 노드와 그 이후 노드들 연결
        if (mapNodes.Count > 1)
        {
            var startCtrl = mapNodes[0][0];      // 맨 위의 스타트 노드
            var nextRow = mapNodes[1];        // 그 아래 행
            int[] branches = { 0, nextRow.Count / 2, nextRow.Count - 1 };
            foreach (var idx in branches)
                DrawDots(startCtrl, nextRow[idx]);
        }
        // boss 노드와 그 전 노드들 연결
        var bossCtrl = mapNodes[^1][0];
        foreach (var prev in mapNodes[^2])
            DrawDots(prev, bossCtrl);

        var pi = Instantiate(playerIndicatorPrefab, stageContainer);
        playerIndicatorRt = pi.GetComponent<RectTransform>();

        UpdateAlphas();


        var camFollow = FindObjectOfType<MapFollowCamera>();
        if (camFollow != null)
            camFollow.target = playerIndicatorRt;

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
        connections.Add($"{a.Row},{a.Col}-{b.Row},{b.Col}");
        connections.Add($"{b.Row},{b.Col}-{a.Row},{a.Col}");
    }


    void UpdateAlphas()
    {
        if (mapNodes == null) return;
        for (int r = 0; r < mapNodes.Count; r++)
            for (int c = 0; c < mapNodes[r].Count; c++)
                mapNodes[r][c]?.SetAlpha((r == currentRow) ? activeAlpha : defaultAlpha);
    }

    public void OnNodeClicked(int r, int c, NodeType t)
    {
        if (!IsNodeReachable(r, c)) { Debug.Log("이동 불가한 노드"); return; }
        currentRow = r; currentCol = c;
        UpdateAlphas();
        playerIndicatorRt.anchoredPosition = mapNodes[r][c]
            .GetComponent<RectTransform>().anchoredPosition;
        switch (t)
        {
            case NodeType.Enemy:
                GameManager.Instance.SetStageInfo(r, StageType.NormalBattle, GameManager.Instance.dungeonLevel);
                SceneController.ToBattle();
                break;
            case NodeType.Boss:
                GameManager.Instance.SetStageInfo(r, StageType.BossBattle, GameManager.Instance.dungeonLevel);
                SceneController.ToBoss();
                break;
            case NodeType.Heal:
                SceneController.ToHeal();
                break;
        }
    }

    public void RestoreMap()
    {
        if (mapNodes == null || playerIndicatorRt == null) return;
        UpdateAlphas();
        playerIndicatorRt.anchoredPosition = mapNodes[currentRow][currentCol]
            .GetComponent<RectTransform>().anchoredPosition;
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}