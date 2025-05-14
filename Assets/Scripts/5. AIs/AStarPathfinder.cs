using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder : MonoBehaviour
{
    public static AStarPathfinder Instance;

    #region Awake 메서드
    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region FindPath 메서드 → 맵 내 길들을 탐색하는 기능
    public List<Vector2Int> FindPath(Vector3 startWorldPos, Vector3 targetWorldPos)
    {
        Vector2Int start = GridScanner.Instance.WorldToCell(startWorldPos);
        Vector2Int target = GridScanner.Instance.WorldToCell(targetWorldPos);

        if (!GridScanner.Instance.IsWalkable(start) || !GridScanner.Instance.IsWalkable(target))
        {
            return new List<Vector2Int>();
        }
        
        Dictionary<Vector2Int, Node> allNodes = new Dictionary<Vector2Int, Node>();
        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        HashSet<Vector2Int> openSetLookup = new HashSet<Vector2Int>();
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();

        Node startNode = new Node(start);
        startNode.GCost = 0;
        startNode.HCost = GetManhattanDistance(start, target);
        allNodes[start] = startNode;

        openSet.Enqueue(startNode);
        openSetLookup.Add(start);

        while (openSet.Count > 0)
        {
            Node current = openSet.Dequeue();
            openSetLookup.Remove(current.Position);

            if (current.Position == target)
            {
                return ReconstructPath(current);
            }

            closedSet.Add(current.Position);

            foreach (Vector2Int neighbourPos in GridScanner.Instance.GetNeighbours(current.Position))
            {
                
                if (closedSet.Contains(neighbourPos))
                    continue;

                int tentativeGCost = current.GCost + 1;

                if (!allNodes.TryGetValue(neighbourPos, out Node neighbourNode))
                {
                    neighbourNode = new Node(neighbourPos);
                    allNodes[neighbourPos] = neighbourNode;
                }

                if (tentativeGCost < neighbourNode.GCost)
                {
                    neighbourNode.GCost = tentativeGCost;
                    neighbourNode.HCost = GetManhattanDistance(neighbourPos, target);
                    neighbourNode.Parent = current;

                    if (!openSetLookup.Contains(neighbourPos))
                    {
                        openSet.Enqueue(neighbourNode);
                        openSetLookup.Add(neighbourPos);
                    }
                }
            }
        }

        return new List<Vector2Int>();
    }

    #endregion

    #region GetManhattanDistance 메서드 

    public static int GetManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    #endregion

    #region ReconstructPath 메서드
    private List<Vector2Int> ReconstructPath(Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node current = endNode;

        while (current != null)
        {
            path.Add(current.Position);
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }

    #endregion
}