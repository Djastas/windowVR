using System.Collections.Generic;
using UnityEngine;

namespace Energy
{
    public class Distributor : EnergyInput
    {
        [SerializeField] private List<Cable> cables;
        public override void SetPower(float value)
        {
            var remainingPower = value;

            foreach (var cable in cables)
            {
                remainingPower -= cable.Output.requiredEnergy;
               
                cable.Output.SetPower(cable.Output.requiredEnergy + (remainingPower<0 ? remainingPower : 0));
            }
            takenEnergy = value - remainingPower;

        }
    }
}