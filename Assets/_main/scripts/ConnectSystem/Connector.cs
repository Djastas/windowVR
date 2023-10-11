using UnityEngine;

namespace _main.scripts.ConnectSystem
{
    public class Connector : MonoBehaviour
    {
        public bool isConnect;
        public FixedJoint fixedJoint;
        
        [SerializeField] private Detail detail;


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
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = targetObject.GetComponent<Rigidbody>();
            joint.anchor = Vector3.zero;
            joint.axis = Vector3.forward;
            return joint;
        }
    }
}