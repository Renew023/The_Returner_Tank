using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScanner : MonoBehaviour
{
    #region GridScanner 객체 변수 선언
    public static GridScanner Instance;
    
    [SerializeField] private LayerMask obstacleLayer;
    
    private Grid grid;
    private Dictionary<Vector2Int, bool> walkableMap = new();

    #endregion

    #region Awake 메서드
    private void Awake()
    {
        Instance = this;
        grid = FindObjectOfType<Grid>();

        BoundsInt bounds = CalculateBoundsFromGridChildren();
        ScanGrid(bounds);
    }

    #endregion

    #region CalculateBoundsFromGridChildren 메서드
    private BoundsInt CalculateBoundsFromGridChildren()
    {
        BoundsInt totalBounds = new BoundsInt();

        Tilemap[] tilemaps = grid.GetComponentsInChildren<Tilemap>();
        if (tilemaps.Length == 0)
        {
            Debug.LogWarning("No Tilemaps found under Grid.");
            return new BoundsInt(-10, -10, 0, 20, 20, 1); // fallback
        }

        bool initialized = false;
        foreach (var tilemap in tilemaps)
        {
            if (!initialized)
            {
                totalBounds = tilemap.cellBounds;
                initialized = true;
            }

            else
            {
                totalBounds.xMin = Mathf.Min(totalBounds.xMin, tilemap.cellBounds.xMin);
                totalBounds.yMin = Mathf.Min(totalBounds.yMin, tilemap.cellBounds.yMin);
                totalBounds.xMax = Mathf.Max(totalBounds.xMax, tilemap.cellBounds.xMax);
                totalBounds.yMax = Mathf.Max(totalBounds.yMax, tilemap.cellBounds.yMax);
            }
        }

        return totalBounds;
    }

    #endregion

    #region ScanGrid 메서드
    public void ScanGrid(BoundsInt bounds)
    {
        walkableMap.Clear();

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector2Int cellPos = new(x, y);
                Vector3 worldPos = CellToWorld(cellPos);
                bool isBlocked = Physics2D.OverlapPoint(worldPos, obstacleLayer);
                walkableMap[cellPos] = !isBlocked;
            }
        }
    }

    #endregion

    #region IsWalkable, WorldToCell, CellToWorld 메서드
    public bool IsWalkable(Vector2Int cellPos)
    {
        return walkableMap.TryGetValue(cellPos, out bool walkable) && walkable;
    }

    public Vector2Int WorldToCell(Vector3 worldPos) =>
        (Vector2Int)grid.WorldToCell(worldPos);

    public Vector3 CellToWorld(Vector2Int cellPos) =>
        grid.CellToWorld((Vector3Int)cellPos) + new Vector3(0.5f, 0.5f);

    #endregion

    #region GetNeighbours 메서드
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

    #endregion
}