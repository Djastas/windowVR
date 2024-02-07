using UnityEngine;

namespace _main.scripts.MapSystem
{
    public class IngredientController : MonoBehaviour
    {
        [SerializeField] private MapData map;
        [SerializeField] private GameObject prefab;
        public string idIngredient;
        
        public string TestCook; // for test
        
        public void Cook(string cookType)
        {
            var node = map.nodes.Find(t => t.id == idIngredient); // find ingredient on map
            var nodeData = map.nodes.Find(t => t.id == DictionaryUtils.Split(node.nextDataIds)[cookType]);
            
            if (node == null)
            {
                Debug.LogError($"cant find '{idIngredient}' in '{map.name}'",this);
                return;
            }
            if (nodeData == null)
            {
                Debug.LogError($"cant find node in out '{cookType}' in node '{idIngredient}' in '{map.name}'",this);
                return;
            }
            
            Debug.Log($"{cookType} {idIngredient} to {nodeData.id}");
            idIngredient = nodeData.id;
            
            var tmpPrefab = Instantiate(nodeData.sceneData.prefab,prefab.transform.position,prefab.transform.rotation,transform); 
            Destroy(prefab);
            prefab = tmpPrefab;

            // var i = DictionaryUtils.Split(node.nextDataIds);
            // SceneManager.LoadScene(i[idCurrentPort]);
            // player.transform.position = 
        }
        [ContextMenu("TestCook")]
        private void Test()
        {
            Cook(TestCook);
        }
    }
}