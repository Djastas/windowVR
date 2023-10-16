using System.Collections.Generic;
using StvDEV.StarterPack;
using UnityEngine;

namespace _main.scripts.managers
{
    public class SessionManager : MonoBehaviourSingleton<SessionManager>
    {
        public float TimeElapsed;
        public List<ConnectData> _connectsData;

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
                ID = id
            };
        }

      
        
    }
    public class ConnectData
    {
        public string ID;
        public FormatTime Time;
    }

    public class FormatTime
    {
        public float Hour;
        public float Minutes;
        public float Seconds;

        public  FormatTime(float TimeElapsed)
        {
            Hour = Mathf.FloorToInt(TimeElapsed / 3600f);
            Minutes = Mathf.FloorToInt(TimeElapsed / 60 - Hour * 3600f);
            Seconds = Mathf.FloorToInt(TimeElapsed - Minutes * 60f);
        }
    }
}