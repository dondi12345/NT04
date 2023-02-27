using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadBehaviour), true)]
public class LoadBehaviourEditor : Editor
{
    private LoadBehaviour loadBehaviour;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        loadBehaviour = (LoadBehaviour) target;

        if(GUILayout.Button("LoadComponents")){
            this.loadBehaviour.LoadComponents();
        }
    }
}
