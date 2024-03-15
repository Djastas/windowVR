using System;
using UnityEngine;

namespace _main.scripts.components
{
    public class StreamController : MonoBehaviour
    {
        
        public FluidComponent parentFluid;
        [SerializeField] private GameObject VisualObject;
        [SerializeField] private float pourPowerScaleMultiplier;

        private void Update()
        {
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out var hit, Mathf.Infinity);
            VisualObject.transform.position = Vector3.Lerp(transform.position,hit.point,0.5f); //set pos like half distance
            
            var localScale = VisualObject.transform.localScale; 
            VisualObject.transform.localScale = new Vector3( parentFluid.pourPower * pourPowerScaleMultiplier,hit.distance,parentFluid.pourPower * pourPowerScaleMultiplier); // set local scale of distance
        }
    }
}