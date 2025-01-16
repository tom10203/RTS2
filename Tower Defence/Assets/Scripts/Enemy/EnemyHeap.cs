using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHeap
{
    List<GameObject> _heap = new List<GameObject>();
    public GameObject rootObj;
    public bool isEmpty => _heap.Count == 0;
    public int Count => _heap.Count;

    GameObject Peek()
    {
        return _heap[0];
    }

    int ParentIndex(int idx)
    {
        return (idx - 1) / 2;
    }
    void AddEnemy(GameObject enemy)
    {
        if (_heap.Count == 0)
        {
            rootObj = enemy;
        }
        else
        {
            int idx = _heap.Count; // Not _heap.Count - 1 as Object is added at end of function
            GameObject parent = _heap[ParentIndex(idx)];
        }
        _heap.Add(enemy);
    }

    void Anchor(GameObject child, GameObject parent)
    {

    }

    void DisableAnchor(GameObject rootObj)
    {
        HingeJoint hinge = rootObj.GetComponent<HingeJoint>();
        
    }
}
