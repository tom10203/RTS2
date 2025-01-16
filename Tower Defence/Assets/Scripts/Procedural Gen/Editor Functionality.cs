using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting.FullSerializer;

//[CustomEditor(typeof(PerlinNoiseChunkGen))]
public class EditorFunctionality : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        //PerlinNoiseChunkGen mapGen = (PerlinNoiseChunkGen)target;

        if (DrawDefaultInspector())
        {
            //if (mapGen.autoUpdate)
            //{
            //    Debug.Log("autoUpdate called");
            //    mapGen.generateTexture();
            //}
        }

        if (GUILayout.Button("Generate"))
        {
            //mapGen.GenerateChunks();
        }

        //if (GUILayout.Button("Left"))
        //{
        //    mapGen.UpdateProceduralInputs(-1, 0);
        //    mapGen.GenerateChunk();
        //}

        //if (GUILayout.Button("Up"))
        //{
        //    mapGen.UpdateProceduralInputs(0, 1);
        //    mapGen.GenerateChunk();
        //}
        //if (GUILayout.Button("Right"))
        //{
        //    mapGen.UpdateProceduralInputs(1, 0);
        //    mapGen.GenerateChunk();
        //}
        //if (GUILayout.Button("Down"))
        //{
        //    mapGen.UpdateProceduralInputs(0, -1);
        //    mapGen.GenerateChunk();
        //}


        //if (GUILayout.Button("Reset"))
        //{
        //    mapGen.ResetValues();
        //}

        if (GUILayout.Button("MathTEST"))
        {
            Debug.Log(Mathf.RoundToInt(3f/2f));
        }
    }
}
