using System;
using System.Collections.Generic;
using UnityEngine;

namespace _main.scripts
{
    [CreateAssetMenu]
    public class MapsData : ScriptableObject
    {
        public List<MapData> mapsData;
        [Serializable]
        public class MapData
        {
            public string id;
            public string name;
            public Sprite preview;
        }
        
    }
    

   
}