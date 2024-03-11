using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _main.scripts
{
    public class CookStationMonitorController : MonoBehaviour
    {
        [SerializeField] private StationController cookStation;
        [SerializeField] private TMP_Text text;
        
        [SerializeField] private Transform group;
        [SerializeField] private List<CookStationMonitorElementUpdater> activeInfoElements;
        [SerializeField] private GameObject infoHolderPrefabs;

        private void Start()
        {
            cookStation.onAddIngredient.AddListener(UpdateMonitor);
            cookStation.onRemoveIngredient.AddListener(UpdateMonitor);
        }

        public void UpdateMonitor()
        {
            foreach (var infoElement in activeInfoElements)
            {
                activeInfoElements.Remove(infoElement);
                Destroy(infoElement.gameObject);
            }


            foreach (var ingredientsTime in cookStation.IngredientsTimes)
            {
                var cookStationMonitorElementUpdater = Instantiate(infoHolderPrefabs, group)
                    .GetComponent<CookStationMonitorElementUpdater>();
                if (cookStationMonitorElementUpdater == null)
                {
                    Debug.LogError("info holder prefab not contain 'CookStationMonitorElementUpdater' ", this);
                    return;
                }

                cookStationMonitorElementUpdater.timeComponent = ingredientsTime.Timer;
                cookStationMonitorElementUpdater.ingredientName = ingredientsTime.Ingredient.idIngredient;
                cookStationMonitorElementUpdater.monitorController = this;
                cookStationMonitorElementUpdater.Init();
                activeInfoElements.Add(cookStationMonitorElementUpdater);
            }
        }
    }
}