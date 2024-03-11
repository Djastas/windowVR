using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _main.scripts.MapSystem
{
    public class IngredientController : MonoBehaviour
    {
        [SerializeField] private MapData map;
        [SerializeField] private GameObject prefab;
        public string idIngredient;
        
        public string tmpCook; 
        [SerializeField] private UnityEvent<string> onEventNode;

        private void Start()
        {
            onEventNode.AddListener( EventSeparator.Instance.InvokeEvent);
        }

        public void Cook(string cookType)
        {
            var node = map.nodes.Find(t => t.id == idIngredient); // find ingredient on map
            NodeData nodeData = null;
            
            try
            {
                 nodeData = map.nodes.Find(t => t.id == DictionaryUtils.Split(node.nextDataIds)[cookType]); // node on out
            }
            catch(Exception e)
            {
                Debug.LogWarning($"cant find cook way to {idIngredient} in {cookType}",this);
                return;
            }

            if (node == null)
            {
                Debug.LogError($"cant find '{idIngredient}' in '{map.name}'",this);
                return;
            }
            
            if (nodeData == null)
            {
                Debug.LogWarning($"cant find node in out '{cookType}' in node '{idIngredient}' in '{map.name}'",this);
                return;
            } 
            
            
            if (nodeData.id == "Event")
            {
                onEventNode?.Invoke(nodeData.eventText);
            }
            
            Debug.Log($"{cookType} {idIngredient} to {nodeData.id}");
            idIngredient = nodeData.id;
            
            var tmpPrefab = Instantiate(nodeData.sceneData.prefab.transform.GetChild(0).gameObject,prefab.transform.position,prefab.transform.rotation,transform); 
            Destroy(prefab);
            prefab = tmpPrefab;
        }
        
        [Button("TestCook")]
        public void TmpCook()
        {
            Cook(tmpCook);
        }
    }
}