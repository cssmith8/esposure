using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardDataManager))]
public class CardDataManagerEditor : Editor {
    private int a = 0;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CardDataManager cdm = (CardDataManager)target;
        
        if (GUILayout.Button("Reimport card data")) {
            cdm.ReimportData();
        }
    }
}
