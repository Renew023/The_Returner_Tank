// Assets/Scripts/MapManager.cs
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [Header("Prefab & Icon 설정")]
    public GameObject nodePrefab;      // NodeController 가 붙어 있는 Prefab
    public Sprite enemyIcon, healIcon, bossIcon;
    public GameObject playerIndicatorPrefab;

    [Header("Layout 설정")]
    public RectTransform stageContainer; // Canvas 내 빈 GameObject
    public int totalRows = 4;            // 0~2 일반/힐, 3 보스
    public int choicesPerRow = 3;
    public float xSpacing = 100f, ySpacing = -100f;
    public byte defaultAlpha = 160;     // (0~255)

    // 내부 데이터
    private static bool mapInitialized = false;
    private static List<List<NodeType>> mapData;
    private List<List<NodeController>> mapNodes;
    private GameObject playerIndicator;
    private int currentRow = 0, currentCol = 0;

    void Start()
    {
        if (!mapInitialized)
        {
            GenerateMapData();              // 최초 한 번만
            mapInitialized = true;
            GameManager.IsFirstMapEntry = false;
        }
        RenderMap();                         // 매번 “그리기”는 호출
    }

    // 1) 난수로 mapData 채우기 (8:2 비율, 마지막 행은 Boss)
    private void GenerateMapData()
    {
        mapData = new List<List<NodeType>>();
        var rnd = new System.Random();

        // 일반/힐 스테이지
        for (int row = 0; row < totalRows - 1; row++)
        {
            var list = new List<NodeType>();
            for (int i = 0; i < choicesPerRow; i++)
            {
                list.Add(rnd.Next(10) < 8 ? NodeType.Enemy : NodeType.Heal);
            }
            mapData.Add(list);
        }

        // 보스 스테이지 (마지막 행)
        mapData.Add(new List<NodeType> { NodeType.Boss });
    }

    // 2) mapData → 씬에 노드로 변환 & 배치
    private void RenderMap()
    {
        // 이전에 만들어둔 노드들 전부 삭제
        if (mapNodes != null)
        {
            foreach (var row in mapNodes)
                foreach (var node in row)
                    Destroy(node.gameObject);
            if (playerIndicator != null) Destroy(playerIndicator);
        }

        mapNodes = new List<List<NodeController>>();

        for (int row = 0; row < mapData.Count; row++)
        {
            int count = mapData[row].Count;
            var rowList = new List<NodeController>();

            for (int col = 0; col < count; col++)
            {
                // Instantiate & 초기화
                var go = Instantiate(nodePrefab, stageContainer);
                var ctrl = go.GetComponent<NodeController>();
                ctrl.Init(row, col, mapData[row][col], this);

                // 아이콘 교체
                var img = go.GetComponent<Image>();
                img.sprite = mapData[row][col] == NodeType.Enemy
                             ? enemyIcon
                             : mapData[row][col] == NodeType.Heal
                               ? healIcon
                               : bossIcon;
                // 기본 투명도
                var c = img.color; c.a = defaultAlpha / 255f; img.color = c;

                // 위치 계산
                float x = (col - (count - 1) / 2f) * xSpacing;
                float y = row * ySpacing;
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

                rowList.Add(ctrl);
            }

            mapNodes.Add(rowList);
        }

        // PlayerIndicator 띄우기
        playerIndicator = Instantiate(playerIndicatorPrefab, stageContainer);
        UpdatePlayerIndicator();
        UpdateSelectable();  // 현재 row 의 노드만 투명도 해제
    }

    // 선택 가능한 노드만 불투명으로
    private void UpdateSelectable()
    {
        for (int r = 0; r < mapNodes.Count; r++)
        {
            for (int c = 0; c < mapNodes[r].Count; c++)
            {
                var img = mapNodes[r][c].GetComponent<Image>();
                var col = img.color;
                bool selectable = (r == currentRow);
                col.a = selectable ? 1f : defaultAlpha / 255f;
                img.color = col;
            }
        }
    }

    // 플레이어 위치 표시
    private void UpdatePlayerIndicator()
    {
        var target = mapNodes[currentRow][currentCol].GetComponent<RectTransform>().anchoredPosition;
        playerIndicator.GetComponent<RectTransform>().anchoredPosition = target;
    }

    // NodeController 에서 호출: 노드 클릭 시
    public void OnNodeClicked(int row, int col, NodeType type)
    {
        currentRow = row;
        currentCol = col;

        // 다음 씬 진입 직전까지 mapData 와 mapInitialized 유지
        if (type == NodeType.Enemy || type == NodeType.Boss)
            SceneController.GoToBattle();
        else
            SceneController.GoToHeal();
    }
}
