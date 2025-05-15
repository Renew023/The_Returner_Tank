using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    #region Node 객체 변수 선언
    public Vector2Int Position;
    public Node Parent;

    public int GCost; // 시작점에서 현재까지의 비용
    public int HCost; // 현재에서 목표까지의 휴리스틱 비용
    public int FCost => GCost + HCost;

    #endregion

    #region Node 객체 생성자 
    public Node(Vector2Int position)
    {
        Position = position;
        GCost = int.MaxValue;
        HCost = 0;
        Parent = null;
    }

    #endregion

    #region Equals, GetHashCode 메서드
    public override bool Equals(object obj)
    {
        return obj is Node node && node.Position == Position;
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode();
    }

    #endregion

    #region CompareTo 메서드

    public int CompareTo(Node other)
    {
        int compare = FCost.CompareTo(other.FCost);
        if (compare == 0) compare = HCost.CompareTo(other.HCost);
        return compare;
    }

    #endregion
}