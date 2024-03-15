using System;
using System.Collections.Generic;
using UnityEngine;

namespace _main.scripts.components
{
    public class ResetTransformComponent : MonoBehaviour
    {
        [SerializeField] private PosRot useForReset;
        private List<float> _values  = new();


        private void Start()
        {
            if ((useForReset & PosRot.PosX) != 0)
            {
                _values.Add(transform.position.x);
            }
            if ((useForReset & PosRot.PosY) != 0)
            {
                _values.Add(transform.position.y);
            }
            if ((useForReset & PosRot.PosZ) != 0)
            {
                _values.Add(transform.position.z);
            }
            
            
            if ((useForReset & PosRot.RotX) != 0)
            {
                _values.Add(transform.rotation.eulerAngles.x);
            }
            if ((useForReset & PosRot.RotY) != 0)
            {
                _values.Add(transform.rotation.eulerAngles.y);
            } if ((useForReset & PosRot.RotZ) != 0)
            {
                _values.Add(transform.rotation.eulerAngles.z);
            }
        }

        private void Update()
        {
            Vector3 pos = new Vector3();
            Vector3 rot = new Vector3();
            int counter = 0;

          
            if ((useForReset & PosRot.PosX) != 0)
            {
                pos.x = _values[counter];
                counter++;
            }
            else
            {
                pos.x = transform.position.x;
            }
            
            if ((useForReset & PosRot.PosY) != 0)
            {
                pos.y = _values[counter];
                counter++;
            } else
            {
                pos.y = transform.position.y;
            }
            
            if ((useForReset & PosRot.PosZ) != 0)
            {
                pos.z = _values[counter];
                counter++;
            } else
            {
                pos.z = transform.position.z;
            }
            
            
            if ((useForReset & PosRot.RotX) != 0)
            {
                rot.x = _values[counter];
                counter++;
            } else
            {
                rot.x = transform.rotation.eulerAngles.x;
            }
            if ((useForReset & PosRot.RotY) != 0)
            {
                rot.y = _values[counter];
                counter++;
            } else
            {
                rot.y = transform.rotation.eulerAngles.y;
            }
            if ((useForReset & PosRot.RotZ) != 0)
            {
                rot.z = _values[counter];
                counter++;
            } else
            {
                rot.z = transform.rotation.eulerAngles.z;
            }

            transform.position = pos;
            transform.rotation = Quaternion.Euler(rot);

        }


        [Flags]
        public enum PosRot
        {
        AllPos = 7,
        PosX = 1,
        PosY = 2,
        PosZ = 4,
        AllRot = 56,
        RotX = 8,
        RotY = 16,
        RotZ =  32,
        }
    }

   
}