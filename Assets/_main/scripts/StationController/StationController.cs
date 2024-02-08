using System;
using System.Collections.Generic;
using System.Linq;
using _main.scripts.components;
using _main.scripts.Data;
using _main.scripts.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _main.scripts
{
    public class StationController :  MonoBehaviour
    {
        public List<IngredientTime> IngredientsTimes = new ();

        
        public UnityEvent onAddIngredient;
        public UnityEvent onRemoveIngredient;

        [SerializeField] private GameObject visualObject;
        public string cookName;
        
        [SerializeField] public CookRecipes recipes;
        
        public virtual void OnIngredientAdd(IngredientController tmpIngredient)
        {
            var tmp = new IngredientTime(tmpIngredient,null);
            IngredientsTimes.Add(tmp);
            
            onAddIngredient?.Invoke();
        }
        
        public virtual void OnIngredientAdd(Collider go)
        {
            var tmpIngredient = go.GetComponent<IngredientController>();
            if (tmpIngredient == null)
            {
                tmpIngredient = go.GetComponentInParent<IngredientController>();
            }

            OnIngredientAdd(tmpIngredient);
        }
        
        public virtual void OnIngredientExit(IngredientController tmpIngredient)
        {
            var ingredientTime = IngredientsTimes.Find(t => t.Ingredient = tmpIngredient);
            
            IngredientsTimes.Remove(ingredientTime);
            onRemoveIngredient?.Invoke();
        }
        
        public virtual void OnIngredientExit(Collider go)
        {
            var tmpIngredient = go.GetComponent<IngredientController>();
            if (tmpIngredient == null)
            {
                tmpIngredient = go.GetComponentInParent<IngredientController>();
            }

            OnIngredientExit(tmpIngredient);
        }

        public Dictionary<string, int> GetIngredients() // get counted data
        {
            var updateData = new Dictionary<string, int>();

            foreach (var cookStationIngredient in IngredientsTimes)
            {
                if (updateData.ContainsKey(cookStationIngredient.Ingredient.idIngredient))
                {
                    updateData[cookStationIngredient.Ingredient.idIngredient] += 1;
                }
                else
                {
                    updateData.Add(cookStationIngredient.Ingredient.idIngredient, 1);
                }
            }

            return updateData;
        }
        
[Button]
        public void Cook()
        {
            if (recipes == null)
            {
                Debug.LogError("set recipes in cook station" , this);
                return;
            }
            
            var ingredients = GetIngredients();
            
            foreach (var recipesRecipe in recipes.Recipes)
            {
                var combine = DictionaryUtils.Combine(DictionaryUtils.DictionaryToStringDictionary(ingredients));
                
                if (recipesRecipe.Recipe.Except(combine).Any()) continue; // if in cookStation has all Recipe
                
                Debug.Log("in cookStation now:");
                foreach (var s in combine)
                {
                    Debug.Log(s);
                }
                
                Debug.Log("in recipes now:");
                foreach (var s in recipesRecipe.Recipe)
                {
                    Debug.Log(s);
                }
                
                
                cookName = recipesRecipe.ResultName; // set cook name

                foreach (var ingredientTime in IngredientsTimes)
                {
                    Destroy(ingredientTime.Ingredient.gameObject); // clear all ingredients
                }

                var tmp = Instantiate(recipesRecipe.Result, visualObject.transform.position, visualObject.transform.rotation); // create visual object
                Destroy(visualObject);

                visualObject = tmp;

                break;
            }
        }
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