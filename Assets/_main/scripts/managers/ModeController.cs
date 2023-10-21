using StvDEV.StarterPack;
using UnityEngine;

namespace _main.scripts.managers
{
    public class ModeController : MonoBehaviourSingleton<ModeController>
    {
        public Mode mode;
        public Color paintColor;

       

        public void SetBuildMode()
        {
            mode = Mode.Connect;
        }
        public void SetRemovedMode()
        {
            mode = Mode.Disconnect;
        } 
        public void SetPaintMode()
        {
            mode = Mode.Paint;
        }
        
        public enum Mode
        {
        Connect,Disconnect,Paint
        }
    }
    
}