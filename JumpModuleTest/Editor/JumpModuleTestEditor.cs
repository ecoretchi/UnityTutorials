using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JumpModuleTest))]
public class JumpModuleTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        JumpModuleTest jumpModuleTest = (JumpModuleTest)target;
        EditorGUILayout.Separator();
        GUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("");
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("    UpdateTrajectory    "))
        {
            jumpModuleTest.UpdateTrajectory();
            //Repaint();
            //EditorUtility.SetDirty(this);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        EditorGUILayout.Separator();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("File PhysicsDataPrefab.json:  ");
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("    Save    "))
        {
            jumpModuleTest.Save();
        }       
        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        EditorGUILayout.Separator();
    }
}
