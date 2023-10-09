using UnityEngine;
using UnityEngine.Events;

namespace components.colliders
{
    public class ColliderEvents3D : MonoBehaviour
    {
        [SerializeField] private string checkTag;

        [SerializeField] private bool isTrigger;
        [SerializeField] private UnityEvent<Collider , GameObject> enterEvent;
        [SerializeField] private UnityEvent<Collider , GameObject> stayEvent;
        [SerializeField] private UnityEvent<Collider , GameObject> exitEvent;


        private void OnCollisionStay(Collision  col)
        {
            if (isTrigger) return;
            if(!CheckObject(col.collider)) return;
            stayEvent.Invoke(col.collider , gameObject);
        }

        private void OnCollisionExit(Collision col)
        {
            if (isTrigger) return;
            if(!CheckObject(col.collider)) return;
            exitEvent.Invoke(col.collider , gameObject);
        }

        private void OnCollisionEnter(Collision col)
        {
            if (isTrigger) return;
            if(!CheckObject(col.collider)) return;
            enterEvent.Invoke(col.collider , gameObject);
        }

        private void OnTriggerStay(Collider col)
        {
            if (!isTrigger) return;
            if(!CheckObject(col)) return;
            enterEvent.Invoke(col , gameObject);
        }

        void OnTriggerEnter(Collider col)
        {
            if (!isTrigger) return;
            if(!CheckObject(col)) return;
            enterEvent.Invoke(col ,gameObject);
        }

        void OnTriggerExit(Collider col)
        {
            if (!isTrigger) return;
            if(!CheckObject(col)) return;
            exitEvent.Invoke(col , gameObject);
        }
    
        public bool CheckObject(Collider col)
        {
            if (col == null) return false;
            if (!(col.CompareTag(checkTag) || checkTag == "")) return false;
            
            return true;

        }
    }
}
