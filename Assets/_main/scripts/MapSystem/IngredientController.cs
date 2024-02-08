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
        
        public string tmpCook; // for test
        [SerializeField] private UnityEvent<string> failEvent;

        
        public void Cook(string cookType)
        {
            var node = map.nodes.Find(t => t.id == idIngredient); // find ingredient on map
            var nodeData = map.nodes.Find(t => t.id == DictionaryUtils.Split(node.nextDataIds)[cookType]); // node on out
            
            
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
                failEvent?.Invoke(nodeData.eventText);
            }
            
            Debug.Log($"{cookType} {idIngredient} to {nodeData.id}");
            idIngredient = nodeData.id;
            
            var tmpPrefab = Instantiate(nodeData.sceneData.prefab,prefab.transform.position,prefab.transform.rotation,transform); 
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