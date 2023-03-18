using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tiles))]
public class TilesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Tiles tiles = target as Tiles;
        
        if (GUI.changed || GUILayout.Button("Generate Tiles"))
        {
            tiles.GenerateTiles();
        }
    }
}