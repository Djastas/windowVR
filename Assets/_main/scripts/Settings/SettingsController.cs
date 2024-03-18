using UnityEngine;

namespace _main.scripts.components.Settings
{
    public class SettingsController : MonoBehaviour
    {
        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }
    }
}