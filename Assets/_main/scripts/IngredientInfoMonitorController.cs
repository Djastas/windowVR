using _main.scripts.MapSystem;
using TMPro;
using UnityEngine;

namespace _main.scripts
{
    public class IngredientInfoMonitorController : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void OnEnable()
        {
            text.text = GetComponentInParent<IngredientController>().idIngredient;
        }
    }
}