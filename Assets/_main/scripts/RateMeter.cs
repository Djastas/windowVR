using Sirenix.OdinInspector;
using UnityEngine;

namespace _main.scripts
{
  public class RateMeter : MonoBehaviour
  {
    [SerializeField] private GameObject idealObject;
    [Button]
    public void Rate(GameObject goToTest)
    {
      var i = GetComponentsInChildren<Collider>();
      foreach (var col in i)
      {
        Debug.Log(col.bounds.size);
      }
     
    }
  
  }
}
