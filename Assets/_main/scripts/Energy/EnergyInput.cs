using System;
using UnityEngine;
using UnityEngine.Events;
using utilities;

namespace Energy
{
    public class EnergyInput : EnergyBase
    {
        [SerializeField] private CallMethod callMethod;
        [SerializeField] private UnityEvent<float> action;

        public float requiredEnergy;
        public float takenEnergy;
        public float valueEnergy;

        private IEnergy _energyInterface;
        
        public virtual void SetPower(float value)
        {
            takenEnergy = value;
            valueEnergy = takenEnergy / requiredEnergy;
            
            switch (callMethod)
            {
                case CallMethod.UnityEvent:
                    action.Invoke(valueEnergy);
                    break;
                case CallMethod.Nothing:
                    break;
                case CallMethod.Interface:
                    _energyInterface ??= gameObject.GetComponent<IEnergy>();;
                    _energyInterface.Energy(value , takenEnergy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}