using System;
using System.Collections.Generic;
using StvDEV.StarterPack;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _main.scripts.managers
{
    public class SessionManager : MonoBehaviourSingleton<SessionManager>
    {
        public float TimeElapsed;
        [SerializeField]public List<ConnectData> _connectsData;
        public UnityEvent onDataChange;

        void Update()
        {
            TimeElapsed += Time.deltaTime;
            // float minutes = Mathf.FloorToInt(TimeElapsed / 60f);
            // float seconds = Mathf.FloorToInt(TimeElapsed - minutes * 60f);
        }

        public void RegConnection(string id)
        {
            var time = new FormatTime(TimeElapsed);
            var tmp = new ConnectData
            {
                Time = time,
                ID = id,
                type = "con"
            };
            _connectsData.Add(tmp);
            onDataChange?.Invoke();
        }
        public void RegDisconnection(string id)
        {
            var time = new FormatTime(TimeElapsed);
            var tmp = new ConnectData
            {
                Time = time,
                ID = id,
                type = "dis"
            };
            _connectsData.Add(tmp);
            onDataChange?.Invoke();
        }

      
        
    }
    [Serializable]
    public class ConnectData
    {
        public string type;
        public string ID;
        public FormatTime Time;
    }
    [Serializable]
    public class FormatTime
    {
        public float Hour;
        public float Minutes;
        public float Seconds;

        public  FormatTime(float TimeElapsed)
        {
            Hour = Mathf.FloorToInt(TimeElapsed / 3600f);
            Minutes =  Mathf.FloorToInt((TimeElapsed - Hour * 3600f) / 60f);;
            Seconds = Mathf.FloorToInt(TimeElapsed - Hour * 3600f - Minutes * 60f);
        }
    }
}