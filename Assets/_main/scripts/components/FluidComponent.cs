using UnityEngine;

namespace _main.scripts.components
{
    public class FluidComponent : MonoBehaviour
    {
        [SerializeField] private float pourThreshold = 45f;
        [SerializeField] private Transform origin;
        [SerializeField] private GameObject particlePrefab;

        private bool _isPouring;
        // private GameObject _instance;
        private void Update()
        {
            bool pourCheck = CalculatePourAngle() < pourThreshold;
            if (pourCheck != _isPouring)
            {
                _isPouring = pourCheck;
                if (_isPouring)
                {
                    Instantiate(particlePrefab, origin);
                    
                }
            }
        }

        private float CalculatePourAngle()
        {
            
            return Vector3.Angle(Vector3.down, transform.forward);
        }
    }
}