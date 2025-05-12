using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;

public class MapManager : MonoBehaviour
{
    private HashSet<string> connections = new HashSet<string>();
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

    // ����� �� �����Ϳ� ����
    static bool initialized = false;
    static List<List<NodeType>> mapData;
    List<List<NodeController>> mapNodes;
    List<Image> dotLines;
    int currentRow = 0, currentCol = 0;

    RectTransform playerIndicatorRt;

    private bool IsNodeReachable(int r, int c)
    {
        if (r <= currentRow) return false;

        string key = $"{currentRow},{currentCol}-{r},{c}";
        return connections.Contains(key);
    }
    void Awake()
    {
        // �̱��� & DDOL
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // �� �ε� �ݹ� ���
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
        // ���� Start�� MapScene���� ȣ��ȴٸ� �ٷ� ����
        if (SceneManager.GetActiveScene().name == "MapScene")
            InitializeOrRestoreMap();
    }

    // ���� ������ �ε�� ���� ȣ��˴ϴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MapScene")
        {
            
            var go = GameObject.Find("Canvas/StageContainer");
            if (go != null)
                stageContainer = go.GetComponent<RectTransform>();
            else
                Debug.LogError("MapManager: 'Canvas/StageContainer'�� ã�� �� �����ϴ�.");

            
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
        // ������ ���� ���� �� ����
        mapData.Add(new List<NodeType> { NodeType.Boss });
    }


    void RenderMap()
    {
        connections.Clear();
        // ���� ���/���� ����
        if (mapNodes != null)
        {
            foreach (var row in mapNodes)
            {
                foreach (var n in row)
                {
                    if (n != null && n.gameObject != null)
                        Destroy(n.gameObject);
                }    
            }    
        }
        if (dotLines != null)
        {
            foreach (var d in dotLines)
            {
                if (d != null && d.gameObject != null)
                    Destroy(d.gameObject);
            }
        }



        mapNodes = new List<List<NodeController>>();
        dotLines = new List<Image>();

        // ��� ��ġ
        for (int r = 0; r < mapData.Count; r++)
        {
            var rowList = new List<NodeController>();
            int count = mapData[r].Count;
            for (int c = 0; c < count; c++)
            {
                var nc = Instantiate(nodePrefab, stageContainer);
                // ������ ����
                Sprite icon = mapData[r][c] switch
                {
                    NodeType.Enemy => enemyIcon,
                    NodeType.Heal => healIcon,
                    _ => bossIcon
                };
                nc.Init(r, c, mapData[r][c], this, icon, defaultAlpha);

                // ��ġ ���
                float y = r * ySpacing + baseYOffset;
                float x = (c - (count - 1) / 2f) * xSpacing;
                nc.GetComponent<RectTransform>()
                  .anchoredPosition = new Vector2(x, y);

                rowList.Add(nc);
            }
            mapNodes.Add(rowList);
        }

        // ���� ����
        for (int r = 0; r < mapNodes.Count - 1; r++)
        {
            var curr = mapNodes[r];
            var next = mapNodes[r + 1];
            for (int i = 0; i < curr.Count && i < next.Count; i++)
                DrawDots(curr[i], next[i]);
        }
        // ������ ���� �� ����
        var bossCtrl = mapNodes[^1][0];
        foreach (var prev in mapNodes[^2])
            DrawDots(prev, bossCtrl);

        // �밢�� ����
        var rnd = new System.Random();
        int maxStart = 0;
        for (int rr = 0; rr < mapNodes.Count - 1; rr++)
            maxStart += mapNodes[rr].Count;

        int branchCount = Mathf.Min(rnd.Next(3, 7), maxStart);
        var diagSet = new HashSet<string>();

        while (diagSet.Count < branchCount)
        {
            int r = rnd.Next(0, mapNodes.Count - 1);
            int cnt = mapNodes[r].Count;
            int c = rnd.Next(0, cnt);

            int nextCnt = mapNodes[r + 1].Count;
            var candidates = new List<int>
            {
                Mathf.Clamp(c, 0, nextCnt - 1)
            };
            if (c > 0 && c - 1 < nextCnt) candidates.Add(c - 1);
            if (c + 1 < nextCnt) candidates.Add(c + 1);

            candidates = candidates.Distinct().ToList();
            int destC = candidates[rnd.Next(candidates.Count)];

            string key = $"{r},{c}-{r + 1},{destC}";
            if (diagSet.Add(key))
            {
                DrawDots(mapNodes[r][c], mapNodes[r + 1][destC]);
            }
        }

        // �÷��̾� �ε������� ���� ��ġ ����
        var pi = Instantiate(playerIndicatorPrefab, stageContainer);
        playerIndicatorRt = pi.GetComponent<RectTransform>();
        playerIndicatorRt.anchoredPosition =
            mapNodes[currentRow][currentCol]
                .GetComponent<RectTransform>().anchoredPosition;

        // ���� �� �ʱ�ȭ
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
        string key1 = $"{a.Row},{a.Col}-{b.Row},{b.Col}";
        string key2 = $"{b.Row},{b.Col}-{a.Row},{a.Col}";
        connections.Add(key1);
        connections.Add(key2);
    }

    void UpdateAlphas()
    {
        for (int r = 0; r < mapNodes.Count; r++)
            for (int c = 0; c < mapNodes[r].Count; c++)
                mapNodes[r][c]
                    .SetAlpha((r == currentRow) ? activeAlpha : defaultAlpha);
    }

    /// <summary>
    /// ��� Ŭ�� �� Scene ��ȯ �� ȣ��˴ϴ�.
    /// </summary>
    public void OnNodeClicked(int r, int c, NodeType t)
    {
        // �̵� ���� ���� �ľ�
        if (!IsNodeReachable(r, c))
        {
            Debug.Log("�̵� �Ұ��� ���");
            return;
        }
        // 1) ���� ����
        currentRow = r;
        currentCol = c;

        // 2) ���� ������Ʈ
        UpdateAlphas();

        // 3) �ε������� ��ġ ����
        playerIndicatorRt.anchoredPosition =
            mapNodes[r][c]
                .GetComponent<RectTransform>().anchoredPosition;

        // 4) �� ��ȯ
        if (t == NodeType.Enemy)
        {
            //  ���� ���� ����
            GameManager.Instance.SetStageInfo(r, StageType.NormalBattle, GameManager.Instance.dungeonLevel);

            //  ���� ������ �̵�
            SceneController.ToBattle();
        }

        else if(t == NodeType.Boss)
        {
            GameManager.Instance.SetStageInfo(r, StageType.NormalBattle, GameManager.Instance.dungeonLevel);

            SceneController.ToBoss();
        }

        else
            SceneController.ToHeal();
    }

    public void RestoreMap()
    {
        // 1) ���� �缳��
        UpdateAlphas();

        // 2) �ε������� �̹� ������ ��� ��ġ�� ���ġ
        if (playerIndicatorRt != null && mapNodes != null)
        {
            playerIndicatorRt.anchoredPosition =
                mapNodes[currentRow][currentCol]
                    .GetComponent<RectTransform>().anchoredPosition;
        }
    }

    void OnDestroy()
    {
        // Editor �Ǵ� �÷��̾� ���� �� �ݹ� ����
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
