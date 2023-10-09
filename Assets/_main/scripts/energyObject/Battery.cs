using Energy;
using UnityEngine;

namespace energyObject
{
    public class Battery : MonoBehaviour ,IEnergy
    {
        public float currentCharge;
        public float requiredCharge;
        public float maxOut;
        
        private Distributor _energyOutput;
        public void Energy(float energy , float takenEnergy)
        {
            currentCharge += takenEnergy / 3000;
            _energyOutput ??= gameObject.GetComponent<Distributor>();
            _energyOutput.SetPower(currentCharge / (requiredCharge / maxOut));
            currentCharge -= _energyOutput.takenEnergy / 3000;
        }

      
    }
}
