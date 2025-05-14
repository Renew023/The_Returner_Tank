using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    public Vector2Int Position;
    public Node Parent;

    public int GCost; // 시작점에서 현재까지의 비용
    public int HCost; // 현재에서 목표까지의 휴리스틱 비용
    public int FCost => GCost + HCost;
    
    public Node(Vector2Int position)
    {
        Position = position;
        GCost = int.MaxValue;
        HCost = 0;
        Parent = null;
    }

    public override bool Equals(object obj)
    {
        return obj is Node node && node.Position == Position;
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode();
    }
    
    public int CompareTo(Node other)
    {
        int compare = FCost.CompareTo(other.FCost);
        if (compare == 0) compare = HCost.CompareTo(other.HCost);
        return compare;
    }
}
