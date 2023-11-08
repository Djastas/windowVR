using System;
using _main.scripts.managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _main.scripts
{
    public class ProgressSliderController : MonoBehaviour
    {
        [SerializeField] private int maxDetail;
        private Slider _slider;
        private void Start()
        {
            _slider = GetComponent<Slider>();
            SessionManager.Instance.onDataChange.AddListener(UpdateUI);
        }
[Button]
        private void UpdateUI()
        {
            var connect = 0.0f;
            var disconnect = 0.0f;

            foreach (var connectData in SessionManager.Instance._connectsData)
            {
                switch (connectData.type)
                {
                    case "con":
                        connect++;
                        break;
                    case "des":
                        disconnect++;
                        break;
                }
            }
            _slider.value = (connect - disconnect) / maxDetail;


        }
    }
}
