using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Connector : MonoBehaviour
{
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
       if (isConnect) return;
       isConnect = true;
       if (_target == null || _caller == null) return; 

       _rb.constraints = RigidbodyConstraints.FreezeAll;
       
       DestroyPredict();
       CalcPosRot(gameObject, _target.gameObject, _caller);
   }
[Button]
    public void UnConnect()
    {
        if (!isConnect) return;
        isConnect = false;
            
        _rb.constraints = RigidbodyConstraints.None;
        
        gameObject.transform.SetParent(Instantiate(parentPrefab , transform.position, transform.rotation).transform);
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
    
}