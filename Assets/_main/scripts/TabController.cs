using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _main.scripts
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private List<TabData> tabs;

        private void Start()
        {
            foreach (var tab in tabs)
            {
                tab.dataTransferEvent.AddListener(SetTab); 
            }
        }

        public void SetTab(string index)
        {
            foreach (var tab in tabs)
            {
                tab.Go.SetActive(tab.Index == index);
            }

        }
        [Serializable]
        private class TabData
        {
            public GameObject Go;
            public string Index;

            [HideInInspector]public UnityEvent< string> dataTransferEvent;
            [Button]
            private void SetThisTab()
            {
                dataTransferEvent?.Invoke(Index);
            }
            

        }
    }
}