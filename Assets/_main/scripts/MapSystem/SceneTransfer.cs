using UnityEngine;
using UnityEngine.SceneManagement;

namespace _main.scripts.MapSystem
{
    public class SceneTransfer : MonoBehaviour
    {
        [SerializeField] private MapData map;
        public string idCurrentPort;

        public void ChangeScene(GameObject player)
        {
            var currentScene = SceneManager.GetActiveScene();
            var node = map.nodes.Find(t => t.id == currentScene.name);
            var i = DictionaryUtils.Split(node.nextDataIds);
            SceneManager.LoadScene(i[idCurrentPort]);
            // player.transform.position = 
        }
    }
}