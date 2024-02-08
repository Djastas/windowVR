using System;
using System.Collections.Generic;
using _main.scripts.components;
using _main.scripts.MapSystem;
using UnityEngine;
using UnityEngine.Events;

namespace _main.scripts
{
    public class CookStationController : MonoBehaviour
    {
        public List<IngredientTime> IngredientsTimes = new ();

        [SerializeField] private float cookTime;
        [SerializeField] private string cookType;
        
        [SerializeField] private float burntTime;
        [SerializeField] private string burntType;

        public UnityEvent onAddIngredient;
        public UnityEvent onRemoveIngredient;
        


        public void OnIngredientCook(Collider go)
        {
            var tmpIngredient = go.GetComponent<IngredientController>();
            if (tmpIngredient == null)
            {
                tmpIngredient = go.GetComponentInParent<IngredientController>();
            }
            tmpIngredient.tmpCook = burntType;

            var tmpTimeComponent = tmpIngredient.gameObject.AddComponent<TimeComponent>();
            tmpTimeComponent.time = burntTime;
            tmpTimeComponent.isPlay = true;

            tmpTimeComponent.onEnd.AddListener(tmpIngredient.TmpCook);

            var tmp = new IngredientTime(tmpIngredient,tmpTimeComponent);
            IngredientsTimes.Add(tmp);
            
            onAddIngredient?.Invoke();
        }

        public void OnIngredientExit(Collider go)
        {
            var tmpIngredient = go.GetComponent<IngredientController>();
            if (tmpIngredient == null)
            {
                tmpIngredient = go.GetComponentInParent<IngredientController>();
            }
            
            var ingredientTime = IngredientsTimes.Find(t => t.Ingredient = tmpIngredient);
            if (ingredientTime.Timer.currentTime < cookTime)
            {
                ingredientTime.Ingredient.Cook(cookType);
            }
            
            Destroy(ingredientTime.Timer);
            IngredientsTimes.Remove(ingredientTime);
            onRemoveIngredient?.Invoke();
        }
[Serializable]
        public class IngredientTime
        {
            public IngredientController Ingredient;
            public TimeComponent Timer;

            public IngredientTime(IngredientController ingredient,TimeComponent timer)
            {
                Ingredient = ingredient;
                Timer = timer;
            }
        }
    }
}