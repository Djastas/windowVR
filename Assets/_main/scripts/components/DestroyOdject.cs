using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelcrew.component
{
    public class DestroyOdject : MonoBehaviour
    {
        [SerializeField] private GameObject _DestroyObject;
        public void Destroy()
        {
            Destroy(_DestroyObject);
        }

    }
}
