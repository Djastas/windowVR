using _main.scripts.MapSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapData))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Edit"))
        {
            var editor = EditorWindow.GetWindow<MapEditorWindows>();
            editor.Init((MapData)target);
        }
        DrawDefaultInspector();
    }
}
