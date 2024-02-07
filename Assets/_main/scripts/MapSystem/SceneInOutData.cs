using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _main.scripts.MapSystem
{
    [CreateAssetMenu]
    public class SceneInOutData : ScriptableObject
    {
        public string sceneName;
        
        public List<string> inObjId;
        public List<string> outObjId;
        
        public GameObject prefab;
    }
}