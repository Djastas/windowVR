using System;
using StvDEV.StarterPack;
using UnityEngine;

namespace _main.scripts.managers
{
    public class ModeController : MonoBehaviourSingleton<ModeController>
    {
        public Mode mode;

        public void SetBuildMode()
        {
            mode = Mode.Connect;
        }
        public void SetRemovedMode()
        {
            mode = Mode.Disconnect;
        }
        public enum Mode
        {
        Connect,Disconnect
        }
    }
    
}