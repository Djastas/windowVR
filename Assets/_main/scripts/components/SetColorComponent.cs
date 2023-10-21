using _main.scripts.managers;
using UnityEngine;

namespace _main.scripts.components
{
    public class SetColorComponent : MonoBehaviour
    {
        [SerializeField] private Color color;
        public void SetColor()
        {
            ModeController.Instance.paintColor = color;
        }
    }
}