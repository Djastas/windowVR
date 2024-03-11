using _main.scripts.components;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _main.scripts
{
    public class CookStationMonitorElementUpdater : MonoBehaviour
    {
        public CookStationMonitorController monitorController;
        public TimeComponent timeComponent;
        public string ingredientName;

        [SerializeField] private TMP_Text ingredientNameText;
        [SerializeField] private Slider progressBar;

        public void Init()
        {
            ingredientNameText.text = ingredientName;
            timeComponent.OnChange ??= new UnityEvent<float>();
            timeComponent.OnChange.AddListener(UpdateTime);
            timeComponent.onEnd = new UnityEvent();
            timeComponent.onEnd.AddListener(monitorController.UpdateMonitor);
        }

        private void UpdateTime(float time)
        {
            progressBar.value = time;
        }
       
    }
}