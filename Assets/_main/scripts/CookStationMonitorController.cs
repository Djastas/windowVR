using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _main.scripts
{
    public class CookStationMonitorController : MonoBehaviour
    {
        [SerializeField] private CookStationController cookStation;
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            cookStation.onAddIngredient.AddListener(UpdateMonitor);
            cookStation.onRemoveIngredient.AddListener(UpdateMonitor);
        }

        private void UpdateMonitor()
        {
            var updateData = UpdateData();
            var tmp = "";
            foreach (var (vName,vCount) in updateData)
            {
                tmp += $"{vName} : {vCount} \n"; // format data 
            }
            text.text = tmp;
        }

        private Dictionary<string, int> UpdateData() // get counted data
        {
            var updateData = new Dictionary<string, int>();
            foreach (var cookStationIngredient in cookStation.IngredientsTimes)
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
    }
}