using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Pixelcrew.component
{
    public class ParticleEventComponent : MonoBehaviour
    {
        [SerializeField] private string checkTag;
        
        public UnityEvent<GameObject> actionGo;
        public UnityEvent<Vector3> actionPos;

        
        private List<ParticleCollisionEvent> collisionEvents;
        private ParticleSystem particle;

        private void Start()
        {
            collisionEvents = new List<ParticleCollisionEvent>();
            particle = GetComponent<ParticleSystem>();
        }

        private void OnParticleCollision(GameObject other)
        {
            if(!CheckObject(other)) return;
            var numCollisionEvents = particle.GetCollisionEvents(other, collisionEvents);

            var i = 0;
            while (i < numCollisionEvents)
            {
                Vector3 pos = collisionEvents[i].intersection;
                
                
                actionGo.Invoke(other);
                actionPos.Invoke(pos);
                i++;
            }
        }
        public bool CheckObject(GameObject col)
        {
            if (col == null) return false;
            if (!( checkTag == "" || col.CompareTag(checkTag))) return false;
            
            return true;

        }
    }
}