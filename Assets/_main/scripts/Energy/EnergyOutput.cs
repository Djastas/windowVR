using UnityEngine;

namespace Energy
{
    public class EnergyOutput : EnergyBase
    {
        public float outPower;
        [SerializeField] private Cable cable;
        private void FixedUpdate()
        {
            cable.Output.SetPower(outPower);
        }
    }
}
