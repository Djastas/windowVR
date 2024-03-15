using System.Collections.Generic;
using UnityEngine;

namespace _main.scripts.components
{
    public class CopyTransformComponent : MonoBehaviour
    { 
        [SerializeField] private ResetTransformComponent.PosRot useForReset;
        [SerializeField] private Transform transformToCopy;
        

        private void Update()
        {
            Vector3 pos = new Vector3();
            Vector3 rot = new Vector3();


            if ((useForReset & ResetTransformComponent.PosRot.PosX) != 0)
            {
                pos.x = transformToCopy.position.x;
                
            }
            else
            {
                pos.x = transform.position.x;
            }
            
            if ((useForReset & ResetTransformComponent.PosRot.PosY) != 0)
            {
                pos.y = transformToCopy.position.y;
                
            } 
            
            if ((useForReset & ResetTransformComponent.PosRot.PosZ) != 0)
            {
                pos.z = transformToCopy.position.z;
                
            } else
            {
                pos.z = transform.position.z;
            }
            
            
            if ((useForReset & ResetTransformComponent.PosRot.RotX) != 0)
            {
                rot.x = transformToCopy.rotation.eulerAngles.x;
                
            } else
            {
                rot.x = transform.rotation.eulerAngles.x;
            }
            if ((useForReset & ResetTransformComponent.PosRot.RotY) != 0)
            {
                rot.y = transformToCopy.rotation.eulerAngles.y;
                
            } else
            {
                rot.y = transform.rotation.eulerAngles.y;
            }
            if ((useForReset & ResetTransformComponent.PosRot.RotZ) != 0)
            {
                rot.z = transformToCopy.rotation.eulerAngles.z;
                
            } else
            {
                rot.z = transform.rotation.eulerAngles.z;
            }

            transform.position = pos;
            transform.rotation = Quaternion.Euler(rot);

        }

    }
}