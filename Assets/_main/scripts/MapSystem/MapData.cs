using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _main.scripts.MapSystem
{
    [CreateAssetMenu]
    public class MapData : ScriptableObject
    {
        public List<NodeData> nodes;
        
        public void Clear()
        {
            nodes = new List<NodeData>();
        }
        
#if UNITY_EDITOR
        public void Persist()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(this)));
        }
#endif
        public void Add(NodeData v)
        {
            nodes.Add(v);
        }
    }
    
[Serializable]
    public class NodeData
    {
        public SceneInOutData sceneData;
        public List<string> portIds;
        public string id;
        public Vector2 position;
        public List<string> nextDataIds;
        public string eventText;
    }
   
}