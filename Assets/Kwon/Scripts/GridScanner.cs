using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScanner : MonoBehaviour
{
    public static GridScanner Instance;
    
    [SerializeField] private LayerMask obstacleLayer;
    
    private Grid grid;
    private Dictionary<Vector2Int, bool> walkableMap = new();

    private void Awake()
    {
        Instance = this;
        grid = FindObjectOfType<Grid>();
        ScanGrid(); // bounds 안 넘겨도 됨
    }
    
    public void ScanGrid()
    {
        walkableMap.Clear();

        Tilemap[] tilemaps = grid.GetComponentsInChildren<Tilemap>();
        HashSet<Vector2Int> scanned = new HashSet<Vector2Int>();

        foreach (var tilemap in tilemaps)
        {
            foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (!tilemap.HasTile(pos)) continue; // 비어있는 셀은 스킵

                Vector2Int cellPos = new(pos.x, pos.y);

                // 이미 검사했으면 중복 검사 생략
                if (scanned.Contains(cellPos)) continue;
                scanned.Add(cellPos);

                Vector3 worldPos = CellToWorld(cellPos);
                bool isBlocked = Physics2D.OverlapPoint(worldPos, obstacleLayer);
                walkableMap[cellPos] = !isBlocked;
            }
        }

        Debug.Log($"Scanned {walkableMap.Count} cells from tilemaps directly.");
    }

    
    public bool IsWalkable(Vector2Int cellPos)
    {
        return walkableMap.TryGetValue(cellPos, out bool walkable) && walkable;
    }

    public Vector2Int WorldToCell(Vector3 worldPos) =>
        (Vector2Int)grid.WorldToCell(worldPos);

    public Vector3 CellToWorld(Vector2Int cellPos) =>
        grid.CellToWorld((Vector3Int)cellPos) + new Vector3(0.5f, 0.5f);
    
    public List<Vector2Int> GetNeighbours(Vector2Int cell)
    {
        List<Vector2Int> neighbours = new();
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (var dir in dirs)
        {
            Vector2Int check = cell + dir;
            if (IsWalkable(check)) neighbours.Add(check);
        }

        return neighbours;
    }
}
