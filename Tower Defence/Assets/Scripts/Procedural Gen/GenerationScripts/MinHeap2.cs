using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MinHeap2 
{
    public List<Node> heap = new List<Node>();
    public int length { get { return heap.Count; } }

    int GetLeftChildIndex(int index)
    {
        return index * 2 + 1;
    }

    int GetRightChildIndex(int index)
    {
        return index * 2 + 2;
    }

    int GetParentIndex(int index)
    {
        return (index - 1) / 2;
    }

    bool HasLeftChild(int index)
    {
        return (index * 2 + 1) < heap.Count;
    }

    bool HasRightChild(int index)
    {
        return (index * 2 + 2) < heap.Count;
    }

    bool HasParent(int index)
    {
        return index > 0;
    }

    void SwapNodes(int index1, int index2)
    {
        if (index1 < 0 || index2 < 0 || index1 >= heap.Count || index2 >= heap.Count)
        {
            Debug.Log($"SwapNodes function. Idx1, Idx2 < 0 {index1}, {index2}");
            Debug.Break();
        }
        int heapIndex1 = heap[index1].heapIndex;
        int heapIndex2 = heap[index2].heapIndex;
        heap[index1].heapIndex = heapIndex2;
        heap[index2].heapIndex = heapIndex1;
        (heap[index1], heap[index2]) = (heap[index2], heap[index1]);
    }

    void HeapifyDown()
    {
        // Check to see if current index has either left or right child. If it does, check to see if either left or right child is greater priority than current index
        int i = 0;
        while (HasLeftChild(i) || HasRightChild(i))
        {
            int indexToSwap = i; 
            if (HasLeftChild(i))
            {
                int leftIndex = GetLeftChildIndex(i);
                if (GetPriorityIndex(leftIndex, indexToSwap) == leftIndex)
                {
                    indexToSwap = leftIndex;
                }
            }
            if (HasRightChild(i))
            {
                int rightIndex = GetRightChildIndex(i);
                if (GetPriorityIndex(rightIndex, indexToSwap) == rightIndex)
                {
                    indexToSwap = rightIndex;
                }
            }

            if (indexToSwap == i) break;  // No swap needed

            SwapNodes(i, indexToSwap);
            i = indexToSwap;
        }
    }

    void HeapifyUp()
    {
        int i = heap.Count - 1;
        heap[i].heapIndex = i;
        while (HasParent(i))
        {
            if (GetPriorityIndex(i, GetParentIndex(i)) == i)
            {
                SwapNodes(i, GetParentIndex(i));
                i = GetParentIndex(i);
            }
            else
            {
                return;
            }
        }
    }


    int GetPriorityIndex(int index1, int index2)
    {
        // returns true if node1 has greater priority, else false
        if (index1 < 0 || index2 < 0 || index1 >= heap.Count || index2 >= heap.Count)
        {
            Debug.Log($"GetPriorityIndex function. Idx1, Idx2 < 0");
            Debug.Break();
        }
        Node node1 = heap[index1];
        Node node2 = heap[index2];

        if (node1.fScore < node2.fScore)
        {
            return index1;
        }
        else if (node2.fScore < node1.fScore)
        {
            return index2;
        }

        return node1.hScore <= node2.hScore ? index1 : index2;

    }

    public Node ReturnMinElement()
    {
        if (heap.Count > 0)
        {
            Node nodeToReturn = heap[0];
            (heap[0], heap[heap.Count - 1]) = (heap[heap.Count - 1], heap[0]);
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown();
            return nodeToReturn;
        }
        else
        {
            Debug.Log($"trying to return element from empty heap");
            return null;
        }
        
    }

    public void AddNode(Node node)
    {
        heap.Add(node);
        HeapifyUp();
    }

    public bool Contains(Node node)
    {
        return heap.Contains(node);
    }

    
}
