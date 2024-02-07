using System;
using System.Collections.Generic;
using _main.scripts.components;
using _main.scripts.MapSystem;
using UnityEngine;

namespace _main.scripts
{
    public class CookStationController : MonoBehaviour
    {
        private List<IngredientController> _ingredients = new();
        private List<TimeComponent> _timers = new();
        [SerializeField] private float cookTime;
        [SerializeField] private string cookType;
        

        public void OnIngredientCook(Collider go)
        {
            var tmpIngredient = go.GetComponent<IngredientController>();
            if (tmpIngredient == null)
            {
                tmpIngredient = go.GetComponentInParent<IngredientController>();
            }
            tmpIngredient.tmpCook = cookType;

            var tmpTimeComponent = tmpIngredient.gameObject.AddComponent<TimeComponent>();
            tmpTimeComponent.time = cookTime;
            tmpTimeComponent.isPlay = true;

            tmpTimeComponent.onEnd.AddListener(tmpIngredient.TmpCook);
            
            _ingredients.Add(tmpIngredient);
            _timers.Add(tmpTimeComponent);
        }
    }
}