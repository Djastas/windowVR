using _main.scripts.MapSystem;
using UnityEngine;

namespace _main.scripts
{
    public class AddIngredientToStation : MonoBehaviour
    {
        [SerializeField] private IngredientController ingredient;

        public void Add(GameObject go)
        {
            go.GetComponent<StationController>().OnIngredientAdd(ingredient);
        }
    }
}