using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _main.scripts.Controllers
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private float delay;
        private void Start()
        {
            StartCoroutine(LoadSceneAsync());
        }

        private IEnumerator LoadSceneAsync()
        {
            var currentScene = SceneManager.GetActiveScene();
            var loadProgress = SceneManager.LoadSceneAsync(GlobalFields.LoadSceneName,LoadSceneMode.Additive);
            loadProgress.allowSceneActivation = false;

            yield return new WaitForSeconds(delay);
            
            loadProgress.allowSceneActivation = true;
            
            while (!loadProgress.isDone)
            {
                yield return null;
            }

       
            
            SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}