using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _main.scripts
{
    public class CookStationMonitorController : MonoBehaviour
    {
        [SerializeField] private StationController cookStation;
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            cookStation.onAddIngredient.AddListener(UpdateMonitor);
            cookStation.onRemoveIngredient.AddListener(UpdateMonitor);
        }

        private void UpdateMonitor()
        {
            var updateData = cookStation.GetIngredients();
            var tmp = "";
            foreach (var (vName,vCount) in updateData)
            {
                tmp += $"{vName} : {vCount} \n"; // format data 
            }
            text.text = tmp;
        }

       
    }
}