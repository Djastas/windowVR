using UnityEngine;

namespace _main.scripts.MapSystem
{
    public class CookComponent : MonoBehaviour
    {
        [SerializeField] private string cookType;

        public void Cook(Collider go)
        {
            var tmp = go.GetComponent<IngredientController>();
            if (tmp == null)
            {
                tmp = go.GetComponentInParent<IngredientController>();
            }

            tmp.Cook(cookType);

        }
        
    }
}