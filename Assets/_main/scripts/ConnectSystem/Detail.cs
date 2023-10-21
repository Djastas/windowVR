using System.Collections.Generic;
using _main.scripts.managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _main.scripts.ConnectSystem
{
    public class Detail : MonoBehaviour
    {
        public string id;
        public GameObject visual;

        [SerializeField] private bool roundYaxis;
        
        [SerializeField] private GameObject parentPrefab;
        [SerializeField] private Material predictMat;
        [SerializeField] private List<Connector> connectors;
        private bool _isPredict; //todo refactor this 
    

        private Collider _target;
        private Connector _caller;
        [SerializeField] private List<Connector> _callers; // use this list to connect 

        private GameObject _predictObject;

        private Rigidbody _rb;
        private GameObject _predictCallerObject;

       

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        [Button]
        public void Connect()
        {
            if (ModeController.Instance.mode == ModeController.Mode.Paint) { Paint(); return; }
            if (ModeController.Instance.mode != ModeController.Mode.Connect) return;
            if (_predictObject == null) return;
            if (_caller.isConnect) return;
            _caller.isConnect = true;
            _isPredict = true;

            // SessionManager.Instance.RegConnection(id);

            DestroyPredict();
            
            CalcPosRot(gameObject, _target.gameObject, _caller.gameObject);
            _caller.fixedJoint = _caller.PhysicsConnect(GetDetailInAllParents(_target.gameObject).gameObject);
        }

        private void Paint()
        {
            visual.GetComponent<Renderer>().material.color = ModeController.Instance.paintColor;
        }

        private Rigidbody GetDetailInAllParents(GameObject go)
        {
            var tmp = go.transform;
            Rigidbody targetDetail = null;
            while (targetDetail == null)
            {
                targetDetail = go.GetComponentInParent<Rigidbody>();
                tmp = tmp.transform.parent;
            }

            return targetDetail;
        }

        [Button]
        public void Disconnect()
        {
            if (ModeController.Instance.mode == ModeController.Mode.Connect) return;
            _isPredict = false;
            foreach (var connector in connectors)
            {
                connector.isConnect = false;
                Destroy(connector != null ? connector.fixedJoint : null);
            }
            

            var instantiate = Instantiate(parentPrefab, transform.position, transform.rotation);
            gameObject.transform.SetParent(instantiate.transform);
        }

        public void Predict(Collider target, Connector caller)
        {
            if (caller.isConnect) return;
            if (!_callers.Contains(caller))
            {
                _callers.Add(caller);
            }
            
            if (_isPredict)return;
            
            _target = target;
            _caller = caller;
            
            if (_predictObject)
            {
                _predictObject = _predictObject;
            }
            else
            {
                _predictObject = Instantiate(visual, gameObject.transform ,false);
                _predictObject.transform.SetParent(gameObject.transform.parent);
                _predictCallerObject = new GameObject("Tmp")
                {
                    transform =
                    {
                        parent = _predictObject.transform,
                        position = caller.transform.position
                    }
                };
            
              
                CalcPosRot(_predictObject, target.gameObject, _predictCallerObject);
            }
            
            
            
            // _predictObject.GetComponent<Renderer>().material = predictMat; // set material
            
        }
        public void DestroyPredict()
        {
            _callers.Remove(_caller);
            Destroy(_predictObject);
            Destroy(_predictCallerObject);
        }
        private void CalcPosRot(GameObject go,GameObject target , GameObject caller)
        {
            // var parent = target.transform.parent;
            //
            // go.transform.SetParent(parent);// set parent to calc rotation
            //
            //
            //
            // var parentEulerAngles = parent.localRotation.eulerAngles; // save long link
            // var transformLocalRotation = new Vector3(0,yAxis,0) - new Vector3(parentEulerAngles.x , parentEulerAngles.y,0); // combine angle
            // go.transform.localRotation =  Quaternion.Euler(transformLocalRotation.x,transformLocalRotation.y,transformLocalRotation.z); // set rotation
            //
            //
            //
            //
            // go.transform.SetParent(parent.parent); // set parent to calc offset
            //
            // var vectorToMove = target.transform.position - caller.transform.position; // calc offset
            // go.transform.position = vectorToMove + transform.position; //move to connector
            //
            //
            //
            // go.transform.SetParent(parent.parent);


            var originalRotateAngle = go.transform.localRotation.eulerAngles.y;
            if (roundYaxis)
            {
                originalRotateAngle =  Mathf.Round( originalRotateAngle / 90) * 90 ; // round y axis to 90degrees
            }
            var deltaRotation = target.transform.rotation * Quaternion.Inverse(caller.transform.rotation);
            go.transform.rotation =deltaRotation * go.transform.rotation;
            go.transform.Rotate(new Vector3(0,originalRotateAngle,0),Space.Self);
            
          
            
            var displacement = target.transform.position - caller.transform.position;
            go.transform.position += displacement;



        }

    }
}
  