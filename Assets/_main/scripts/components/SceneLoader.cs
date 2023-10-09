using UnityEngine;
using UnityEngine.SceneManagement;

namespace _main.scripts.components
{
    public class SceneLoader : MonoBehaviour
    {
        public void Load(string name)
        {
            SceneManager.LoadSceneAsync(name);
        }
    }
}
