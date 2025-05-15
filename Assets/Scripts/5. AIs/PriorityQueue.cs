using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : System.IComparable<T>
{
    #region PriorityQueue 객체 변수 선언
    private List<T> heap = new List<T>();

    public int Count => heap.Count;

    #endregion

    #region Enqueue, Dequeue 메서드 
    public void Enqueue(T item)
    {
        heap.Add(item);
        int c = heap.Count - 1;
        while (c > 0)
        {
            int p = (c - 1) / 2;
            if (heap[c].CompareTo(heap[p]) >= 0) break;
            (heap[c], heap[p]) = (heap[p], heap[c]);
            c = p;
        }
    }

    public T Dequeue()
    {
        int li = heap.Count - 1;
        T frontItem = heap[0];
        heap[0] = heap[li];
        heap.RemoveAt(li);

        --li;
        int p = 0;
        while (true)
        {
            int c1 = p * 2 + 1;
            if (c1 > li) break;
            int c2 = c1 + 1;
            int min = (c2 <= li && heap[c2].CompareTo(heap[c1]) < 0) ? c2 : c1;

            if (heap[p].CompareTo(heap[min]) <= 0) break;
            (heap[p], heap[min]) = (heap[min], heap[p]);
            p = min;
        }

        return frontItem;
    }

    #endregion

    #region Contains 메서드
    public bool Contains(T item)
    {
        return heap.Contains(item);
    }

    #endregion
}