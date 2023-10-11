using _main.scripts.managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _main.scripts.ConnectSystem
{
    public class Detail : MonoBehaviour
    {
       
        [SerializeField] private bool roundYaxis;
        

        [SerializeField] private GameObject parentPrefab;
        [SerializeField] private GameObject visual;
        [SerializeField] private Material predictMat;
    

        private Collider _target;
        private Connector _caller;

        private GameObject _predictObject;

        private Rigidbody _rb;
        


        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        [Button]
        public void Connect()
        {
            if (ModeController.Instance.mode == ModeController.Mode.Disconnect) return;
            if (_predictObject == null) return;
            if (_caller.isConnect) return;
            _caller.isConnect = true;

            DestroyPredict();
            
            CalcPosRot(gameObject, _target.gameObject, _caller.gameObject);
            _caller.fixedJoint = _caller.PhysicsConnect(_target.gameObject.transform.parent.gameObject);
        }

        [Button]
        public void Disconnect()
        {
            if (ModeController.Instance.mode == ModeController.Mode.Connect) return;
            if (!_caller.isConnect) return;
            _caller.isConnect = false;
        
            Destroy(_caller.fixedJoint);

            var instantiate = Instantiate(parentPrefab, transform.position, transform.rotation);
            gameObject.transform.SetParent(instantiate.transform);
        }
        
        public void Predict(Collider target , Connector caller)
        { 
            if (caller.isConnect) return;
            
            _target = target;
            _caller = caller;
        
            _predictObject = _predictObject 
                ? _predictObject 
                : Instantiate(visual , gameObject.transform, false); // checking for null
        
            _predictObject.GetComponent<Renderer>().material = predictMat; // set material
        

            CalcPosRot(_predictObject,target.gameObject,caller.gameObject);
        
        }
        public void DestroyPredict()
        {
            Destroy(_predictObject);
        }
        private void CalcPosRot(GameObject go,GameObject target , GameObject caller)
        {
            var parent = target.transform.parent;
        
            go.transform.SetParent(parent.parent); // set parent to calc offset
        
            var vectorToMove = target.transform.position - caller.transform.position; // calc offset
            go.transform.position = vectorToMove + transform.position; //move to connector
        
            go.transform.SetParent(target.transform.parent);// set parent to calc rotation
        
            var yAxis = roundYaxis ?  Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90  : transform.localRotation.eulerAngles.y ; // round y axis to 90degrees
        
            var localRotationEulerAngles = parent.localRotation.eulerAngles; // save long link
            var transformLocalRotation = new Vector3(0,yAxis,-180) - new Vector3(localRotationEulerAngles.x , localRotationEulerAngles.y,0); // combine angle
            go.transform.localRotation =  Quaternion.Euler(transformLocalRotation.x,transformLocalRotation.y,transformLocalRotation.z); // set rotation
        
        
            go.transform.SetParent(parent.parent);
        }
        
    }
}
  