using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [Header("Prefabs & Icons")]
    public NodeController nodePrefab;
    public Image dotPrefab;
    public Sprite enemyIcon, healIcon, bossIcon;
    public GameObject playerIndicatorPrefab;

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

    // 맵 데이터 & 상태
    static bool initialized = false;
    static List<List<NodeType>> mapData;
    List<List<NodeController>> mapNodes;
    List<Image> dotLines;
    int currentRow = 0, currentCol = 0;

    // 플레이어 선택 흐름 상태
    int prevRow = -1, prevCol = -1;
    RectTransform playerIndicatorRt;
    bool isIndicatorActive = false;
    HashSet<string> connections = new HashSet<string>();

    private bool IsNodeReachable(int r, int c)
    {
        if (r <= currentRow) return false;
        string key = $"{currentRow},{currentCol}-{r},{c}";
        return connections.Contains(key);
    }

    void Awake()
    {
        // 싱글톤 & DDOL
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

        // 플레이어 인디케이터 미리 생성 후 비활성화
        var pi = Instantiate(playerIndicatorPrefab, stageContainer);
        playerIndicatorRt = pi.GetComponent<RectTransform>();
        playerIndicatorRt.gameObject.SetActive(false);
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
        connections.Clear();
        // (1) 기존 노드/점선 삭제
        if (mapNodes != null)
            foreach (var row in mapNodes)
                foreach (var n in row)
                    if (n != null && n.gameObject != null)
                        Destroy(n.gameObject);

        if (dotLines != null)
            foreach (var d in dotLines)
                if (d != null && d.gameObject != null)
                    Destroy(d.gameObject);

        // (2) 새 리스트 초기화
        mapNodes = new List<List<NodeController>>();
        dotLines = new List<Image>();

        // (3) 노드 배치
        for (int r = 0; r < mapData.Count; r++)
        {
            var rowList = new List<NodeController>();
            int count = mapData[r].Count;
            for (int c = 0; c < count; c++)
            {
                var nc = Instantiate(nodePrefab, stageContainer);
                Sprite icon = mapData[r][c] switch
                {
                    NodeType.Enemy => enemyIcon,
                    NodeType.Heal => healIcon,
                    _ => bossIcon
                };
                nc.Init(r, c, mapData[r][c], this, icon, defaultAlpha);

                float y = r * ySpacing + baseYOffset;
                float x = (c - (count - 1) / 2f) * xSpacing;
                nc.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

                rowList.Add(nc);
            }
            mapNodes.Add(rowList);
        }

        // (4) 세로 연결
        for (int r = 0; r < mapNodes.Count - 1; r++)
        {
            var curr = mapNodes[r];
            var next = mapNodes[r + 1];
            for (int i = 0; i < curr.Count && i < next.Count; i++)
                DrawDots(curr[i], next[i]);
        }

        // (5) 보스 연결
        var bossCtrl = mapNodes[^1][0];
        foreach (var prev in mapNodes[^2])
            DrawDots(prev, bossCtrl);

        // (6) 대각선 랜덤 연결
        var rnd2 = new System.Random();
        int maxStart = mapNodes.Sum(row => row.Count);
        int branchCount = Mathf.Min(rnd2.Next(3, 7), maxStart);
        var diagSet = new HashSet<string>();
        while (diagSet.Count < branchCount)
        {
            int r = rnd2.Next(0, mapNodes.Count - 1);
            int c = rnd2.Next(0, mapNodes[r].Count);
            int nextCnt = mapNodes[r + 1].Count;

            var candidates = new List<int> { Mathf.Clamp(c, 0, nextCnt - 1) };
            if (c > 0) candidates.Add(c - 1);
            if (c + 1 < nextCnt) candidates.Add(c + 1);

            candidates = candidates.Distinct().ToList();
            int destC = candidates[rnd2.Next(candidates.Count)];
            string key = $"{r},{c}-{r + 1},{destC}";
            if (diagSet.Add(key))
                DrawDots(mapNodes[r][c], mapNodes[r + 1][destC]);
        }

        // (7) 알파값 초기화 & 카메라 타겟
        UpdateAlphas();
        FindObjectOfType<MapFollowCamera>().target = playerIndicatorRt;
    }

    void DrawDots(NodeController a, NodeController b)
    {
        Vector2 pa = a.GetComponent<RectTransform>().anchoredPosition;
        Vector2 pb = b.GetComponent<RectTransform>().anchoredPosition;
        for (int i = 1; i < dotSegments; i++)
        {
            float t = i / (float)dotSegments;
            Vector2 pos = Vector2.Lerp(pa, pb, t);
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
        {

            for (int c = 0; c < mapNodes[r].Count; c++)
                mapNodes[r][c].SetAlpha((r == currentRow) ? activeAlpha : defaultAlpha);
        }
    }

    public void OnNodeClicked(int r, int c, NodeType type)
    {
        if (mapNodes == null || mapNodes.Count == 0) return;

        // 첫 클릭: 0행(맨 아래)에서만
        if (!isIndicatorActive)
        {
            if (r != 0) return;
            Vector2 firstPos = mapNodes[r][c].GetComponent<RectTransform>().anchoredPosition;
            playerIndicatorRt.gameObject.SetActive(true);
            playerIndicatorRt.anchoredPosition = firstPos;
            prevRow = r; prevCol = c;
            isIndicatorActive = true;
            return;
        }

        // 이동 가능 체크
        if (!IsNodeReachable(r, c))
        {
            Debug.Log("이동 불가한 노드입니다.");
            return;
        }

        // 연결선
        DrawDots(mapNodes[prevRow][prevCol], mapNodes[r][c]);

        // 인디케이터 이동
        Vector2 targetPos = mapNodes[r][c].GetComponent<RectTransform>().anchoredPosition;
        playerIndicatorRt.anchoredPosition = targetPos;

        // 씬 전환
        if (type == NodeType.Heal)
            SceneController.ToHeal();
        else
        {
            GameManager.Instance.SetStageInfo(
                r,
                type == NodeType.Boss ? StageType.BossBattle : StageType.NormalBattle,
                GameManager.Instance.dungeonLevel
            );
            SceneController.ToBattle();
        }

        prevRow = r; prevCol = c;
    }

    void RestoreMap()
    {
        if (mapNodes == null || playerIndicatorRt == null) return;
        UpdateAlphas();
        playerIndicatorRt.anchoredPosition =
            mapNodes[currentRow][currentCol].GetComponent<RectTransform>().anchoredPosition;
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
