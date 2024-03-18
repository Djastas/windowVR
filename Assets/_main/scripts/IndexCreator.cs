using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _main.scripts
{
    public class IndexCreator : MonoBehaviour
    {
        [SerializeField] private Transform createPosition;
        [SerializeField] private List<IndexedObject<GameObject>> gameObjects;
        [Button]
       
        public void Create(string value)
        {
            var prefab = gameObjects.Find(t => t.key == value)?.value;
            Instantiate(prefab, createPosition.position,createPosition.rotation);
        }
    }

    [Serializable]
    public class IndexedObject<T>
    {
        public string key;
        public T value;
    }
}