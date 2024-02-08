using System;
using System.Collections.Generic;
using UnityEngine;

namespace _main.scripts.Data
{
    [CreateAssetMenu]
    public class CookRecipes : ScriptableObject
    {
        public List<CookRecipe> Recipes;
    }
[Serializable]
    public class CookRecipe
    {
        public List<string> Recipe;
        public string ResultName;
        public GameObject Result;
    }
    
}