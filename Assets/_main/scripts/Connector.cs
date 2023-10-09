using _main.scripts.managers;
using HurricaneVR.Framework.Core;
using Sirenix.OdinInspector;
using UnityEngine;

public class Connector : MonoBehaviour
{
    // todo move connect to connector 
    [SerializeField] private bool isConnect;
    [SerializeField] private bool roundYaxis;

    [SerializeField] private GameObject parentPrefab;
    [SerializeField] private GameObject visual;
    [SerializeField] private Material predictMat;
    

    private Collider _target;
    private GameObject _caller;

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
        if (isConnect) return;
        isConnect = true;
        if (_predictObject == null) return;
        

      
        
        DestroyPredict();
        CalcPosRot(gameObject, _target.gameObject, _caller);
        PhysicsConnect(_target.gameObject.transform.parent.gameObject);
    }
[Button]
    public void UnConnect()
    {
        if (ModeController.Instance.mode == ModeController.Mode.Connect) return;
        if (!isConnect) return;
        isConnect = false;
        

        _rb.constraints = RigidbodyConstraints.None;

        var transform1 = transform; // todo refactor this
        gameObject.transform.SetParent(Instantiate(parentPrefab , transform1.position, transform1.rotation).transform);
    }

    public void Predict(Collider target , GameObject caller)
    {
        if (isConnect) return;
       
        
        _target = target;
        _caller = caller;
        
        _predictObject = _predictObject ? _predictObject: Instantiate(visual , gameObject.transform, false); // checking for null
        
        _predictObject.GetComponent<Renderer>().material = predictMat; // set material
        CalcPosRot(_predictObject,target.gameObject,caller);
    }
    public void DestroyPredict(Collider target , GameObject caller)
    {
        Destroy(_predictObject);
    }
    public void DestroyPredict()
    {
        Destroy(_predictObject);
    }

    
    private void CalcPosRot(GameObject go,GameObject target , GameObject caller)
    {
        go.transform.SetParent(target.transform.parent.parent);
       
        var yAxis = roundYaxis ?  Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90  : transform.localRotation.eulerAngles.y ;
       
        go.transform.localRotation = Quaternion.Euler(0,yAxis,-180); // rotate to connector
     
       
        var vectorToMove = target.transform.position - caller.transform.position;
        go.transform.position = vectorToMove + transform.position; //move to connector
    }
    
    void PhysicsConnect(GameObject targetObject)
    {
        // Create a new FixedJoint and set its properties
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = targetObject.GetComponent<Rigidbody>();
        joint.anchor = Vector3.zero;
        joint.axis = Vector3.forward;

    }
    
}
