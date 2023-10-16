
using _main.scripts.managers;
using Sinbad;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _main.scripts
{
    public class SaveAllDataComponent : MonoBehaviour
    {
        [Button]
        public void SaveAllData()
        {
            CsvUtil.SaveObject(SessionManager.Instance._connectsData,"TestSave.csv");

        }
    }
}