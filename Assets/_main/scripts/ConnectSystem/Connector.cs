using System;
using _main.scripts.managers;
using UnityEngine;

namespace _main.scripts.ConnectSystem
{
    public class Connector : MonoBehaviour
    {
        public bool isConnect;
        public FixedJoint fixedJoint;
        
        public Detail detail;


        public void Predict(Collider target)
        {
           
                detail.Predict(target,this);
            
        }
        public void DestroyPredict()
        {
            detail.DestroyPredict();
        }
        
       
        public FixedJoint PhysicsConnect(GameObject targetObject)
        {
            // Create a new FixedJoint and set its properties
            FixedJoint joint = detail.gameObject.AddComponent<FixedJoint>();
            joint.breakForce = SessionManager.Instance.breakJoints ? 1000 : float.PositiveInfinity;
            joint.connectedBody = targetObject.GetComponent<Rigidbody>();
            joint.anchor = Vector3.zero;
            joint.axis = Vector3.forward;
            return joint;
        }
    }
}