using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapNode
{
    public float Heuristic { get; }
    public float GScore { get; }
    public Vector3 Position { get; }

    public HeapNode(float heuristic, float gScore, Vector3 position)
    {
        Heuristic = heuristic;
        GScore = gScore;
        Position = position;
    }
}

public class MinHeap
{
    private readonly List<HeapNode> _heap = new List<HeapNode>();

    public int Count => _heap.Count;
    public bool IsEmpty => Count == 0;

    public void Add(float heuristic, float gScore, Vector3 position)
    {
        var node = new HeapNode(heuristic, gScore, position);
        _heap.Add(node);
        HeapifyUp(Count - 1);
    }

    public HeapNode RemoveMin()
    {
        if (IsEmpty)
        {
            throw new System.InvalidOperationException("Heap is empty");
        }

        var minNode = _heap[0];
        var lastNode = _heap[Count - 1];
        _heap[0] = lastNode;
        _heap.RemoveAt(Count - 1);

        if (!IsEmpty)
        {
            HeapifyDown(0);
        }

        return minNode;
    }

    public HeapNode Peek()
    {
        if (IsEmpty)
        {
            throw new System.InvalidOperationException("Heap is empty");
        }

        return _heap[0];
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (_heap[index].Heuristic >= _heap[parentIndex].Heuristic)
            {
                break;
            }
            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void HeapifyDown(int index)
    {
        while (true)
        {
            int smallest = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;

            if (leftChild < Count && _heap[leftChild].Heuristic < _heap[smallest].Heuristic)
            {
                smallest = leftChild;
            }

            if (rightChild < Count && _heap[rightChild].Heuristic < _heap[smallest].Heuristic)
            {
                smallest = rightChild;
            }

            if (smallest == index)
            {
                break;
            }

            Swap(index, smallest);
            index = smallest;
        }
    }

    private void Swap(int i, int j)
    {
        var temp = _heap[i];
        _heap[i] = _heap[j];
        _heap[j] = temp;
    }

    public void Clear()
    {
        _heap.Clear();
    }
}