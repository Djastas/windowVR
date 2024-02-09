using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _main.scripts
{
    public class EventSeparator : MonoBehaviour
    {
        public static EventSeparator Instance;

        [SerializeField] private List<CustomEvent> events;
        private void Awake()
        {
            Instance = this;
        }

        public void InvokeEvent(string value)
        {
            events.Find(t => t.Key == value)?.Value?.Invoke();
        }
        
        
    }
[Serializable]
    public class CustomEvent
    {
        public string Key;
        public UnityEvent Value;
    }
}