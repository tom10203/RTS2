using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 position = Vector3.zero;
    public float radius = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] test = Physics.OverlapSphere(position, radius);

        if (test.Length == 0)
        {
            print($"no collision detected");
        }
        else
        {
            print($"collision detected");
        }



    }
}
