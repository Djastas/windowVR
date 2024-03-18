using _main.scripts.components;
using _main.scripts.MapSystem;
using UnityEngine;


namespace _main.scripts
{
    public class CookStationController : StationController
    {
        [SerializeField] private float cookTime;
        [SerializeField] private string cookType;
        
        [SerializeField] private float burntTime;
        [SerializeField] private string burntType;
        

        public override void OnIngredientAdd(IngredientController tmpIngredient)
        {
            // if (autoCook)
            // {
            //     Cook();
            // }
           
            
            tmpIngredient.tmpCook = burntType;

            var tmpTimeComponent = tmpIngredient.gameObject.AddComponent<TimeComponent>();
            tmpTimeComponent.time = burntTime;
            tmpTimeComponent.isPlay = true;

            tmpTimeComponent.onEnd.AddListener(tmpIngredient.TmpCook);

            var tmp = new IngredientTime(tmpIngredient,tmpTimeComponent);
            IngredientsTimes.Add(tmp);
            
            onAddIngredient?.Invoke();
        }
        

        public override void OnIngredientExit(Collider go)
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

    }
}