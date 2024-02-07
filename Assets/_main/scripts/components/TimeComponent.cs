using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _main.scripts.components
{
    public class TimeComponent : MonoBehaviour
    {
        public float time;
        public bool isPlay;
        
        [ReadOnly] public float currentTime;
        
        public UnityEvent onEnd = new();
        private bool _isEnd;
        private void Start()
        {
            currentTime = time;
        }

        private void Update()
        {
            if (!isPlay) { return; }
            
            if (currentTime <= 0)
            {
                if (_isEnd) return;
                onEnd?.Invoke();
                _isEnd = true;
                return;
            }
            
            currentTime -= Time.deltaTime;
        }
    }
}