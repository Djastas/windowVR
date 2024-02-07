using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace components.colliders
{
    public class ColliderEvents3D : MonoBehaviour
    {
        [SerializeField] private string checkTag;

        [SerializeField] private bool isTrigger;
        [SerializeField] private bool oneOnObject;
        
        [SerializeField] private UnityEvent<Collider> enterEvent;
        [SerializeField] private UnityEvent<Collider> stayEvent;
        [SerializeField] private UnityEvent<Collider> exitEvent;
        

        private List<GameObject> _useGameObjects = new();


        private void OnCollisionStay(Collision  col)
        {
            if (isTrigger) return;
            if(!CheckObject(col.collider)) return;
            stayEvent.Invoke(col.collider);
        }

        private void OnCollisionExit(Collision col)
        {
            if (isTrigger) return;
            if(!CheckObject(col.collider)) return;
            exitEvent.Invoke(col.collider);
        }

        private void OnCollisionEnter(Collision col)
        {
            if (isTrigger) return;
            if(!CheckObject(col.collider)) return;
            enterEvent?.Invoke(col.collider);
        }

        private void OnTriggerStay(Collider col)
        {
            if (!isTrigger) return;
            if(!CheckObject(col)) return;
            enterEvent?.Invoke(col);
        }

        void OnTriggerEnter(Collider col)
        {
            if (!isTrigger) return;
            if(!CheckObject(col)) return;
            enterEvent?.Invoke(col);
        }

        void OnTriggerExit(Collider col)
        {
            if (!isTrigger) return;
            if(!CheckObject(col)) return;
            exitEvent.Invoke(col);
        }
    
        public bool CheckObject(Collider col)
        {
            if (col == null) return false;
            if (!(col.CompareTag(checkTag) || checkTag == "")) return false;
            if (oneOnObject)
            {
                if (_useGameObjects.Contains(col.gameObject))
                {
                    return false;
                }
                _useGameObjects.Add(col.gameObject);
            }
            return true;
        }
        
    }
}
