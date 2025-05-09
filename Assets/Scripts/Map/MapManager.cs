// Assets/Scripts/MapManager.cs
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [Header("Prefab & Icon ����")]
    public GameObject nodePrefab;      // NodeController �� �پ� �ִ� Prefab
    public Sprite enemyIcon, healIcon, bossIcon;
    public GameObject playerIndicatorPrefab;

    [Header("Layout ����")]
    public RectTransform stageContainer; // Canvas �� �� GameObject
    public int totalRows = 4;            // 0~2 �Ϲ�/��, 3 ����
    public int choicesPerRow = 3;
    public float xSpacing = 100f, ySpacing = -100f;
    public byte defaultAlpha = 160;     // (0~255)

    // ���� ������
    private static bool mapInitialized = false;
    private static List<List<NodeType>> mapData;
    private List<List<NodeController>> mapNodes;
    private GameObject playerIndicator;
    private int currentRow = 0, currentCol = 0;

    void Start()
    {
        if (!mapInitialized)
        {
            GenerateMapData();              // ���� �� ����
            mapInitialized = true;
            GameManager.IsFirstMapEntry = false;
        }
        RenderMap();                         // �Ź� ���׸��⡱�� ȣ��
    }

    // 1) ������ mapData ä��� (8:2 ����, ������ ���� Boss)
    private void GenerateMapData()
    {
        mapData = new List<List<NodeType>>();
        var rnd = new System.Random();

        // �Ϲ�/�� ��������
        for (int row = 0; row < totalRows - 1; row++)
        {
            var list = new List<NodeType>();
            for (int i = 0; i < choicesPerRow; i++)
            {
                list.Add(rnd.Next(10) < 8 ? NodeType.Enemy : NodeType.Heal);
            }
            mapData.Add(list);
        }

        // ���� �������� (������ ��)
        mapData.Add(new List<NodeType> { NodeType.Boss });
    }

    // 2) mapData �� ���� ���� ��ȯ & ��ġ
    private void RenderMap()
    {
        // ������ ������ ���� ���� ����
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
                // Instantiate & �ʱ�ȭ
                var go = Instantiate(nodePrefab, stageContainer);
                var ctrl = go.GetComponent<NodeController>();
                ctrl.Init(row, col, mapData[row][col], this);

                // ������ ��ü
                var img = go.GetComponent<Image>();
                img.sprite = mapData[row][col] == NodeType.Enemy
                             ? enemyIcon
                             : mapData[row][col] == NodeType.Heal
                               ? healIcon
                               : bossIcon;
                // �⺻ ����
                var c = img.color; c.a = defaultAlpha / 255f; img.color = c;

                // ��ġ ���
                float x = (col - (count - 1) / 2f) * xSpacing;
                float y = row * ySpacing;
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

                rowList.Add(ctrl);
            }

            mapNodes.Add(rowList);
        }

        // PlayerIndicator ����
        playerIndicator = Instantiate(playerIndicatorPrefab, stageContainer);
        UpdatePlayerIndicator();
        UpdateSelectable();  // ���� row �� ��常 ���� ����
    }

    // ���� ������ ��常 ����������
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

    // �÷��̾� ��ġ ǥ��
    private void UpdatePlayerIndicator()
    {
        var target = mapNodes[currentRow][currentCol].GetComponent<RectTransform>().anchoredPosition;
        playerIndicator.GetComponent<RectTransform>().anchoredPosition = target;
    }

    // NodeController ���� ȣ��: ��� Ŭ�� ��
    public void OnNodeClicked(int row, int col, NodeType type)
    {
        currentRow = row;
        currentCol = col;

        // ���� �� ���� �������� mapData �� mapInitialized ����
        if (type == NodeType.Enemy || type == NodeType.Boss)
            SceneController.GoToBattle();
        else
            SceneController.GoToHeal();
    }
}
