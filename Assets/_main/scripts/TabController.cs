using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _main.scripts
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private List<TabData> tabs;
        
        [Button]
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
        }
    }
}