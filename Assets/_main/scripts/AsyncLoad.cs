using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AsyncLoad: MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent loadAction;
    private AsyncOperation _asyncOperation;
    private AsyncOperation _asyncEnvironmentScene;
    private bool _isUp;
    private static readonly int IsUp = Animator.StringToHash("isUP");

    [Button]
    public void AsyncLoading(string sceneIndex,string environmentSceneIndex)
    {
        DontDestroyOnLoad(this);
        animator.SetBool("IsUp",false);
        StartCoroutine(Async(sceneIndex ,environmentSceneIndex));

    }

    IEnumerator Async(string sceneIndex,string environmentSceneIndex)
    {        
        _asyncOperation = SceneManager.LoadSceneAsync(sceneIndex); // load main scene

        
        _asyncEnvironmentScene = SceneManager.LoadSceneAsync(environmentSceneIndex,LoadSceneMode.Additive); // load environment scene
        
        
        loadAction.Invoke();
        while (!_asyncOperation.isDone && !_asyncEnvironmentScene.isDone)
        {
            yield return null;
        }
      
        yield return new WaitForSeconds(4);
        animator.SetBool("IsUp",true);
        Debug.Log("Load Complete");
        _asyncOperation.allowSceneActivation = true;
        _asyncEnvironmentScene.allowSceneActivation = true;
    }
}
