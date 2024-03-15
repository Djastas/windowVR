using System;
using System.Collections.Generic;
using _main.scripts.MapSystem;
using UnityEngine;

namespace _main.scripts.components
{
    public class FluidComponent : MonoBehaviour
    {
        public  float pourPower;
        
        [SerializeField] private float pourThreshold = 45f;

        [SerializeField] private float radius;
        [SerializeField] private float upOffset;
        [SerializeField] private Vector3 rotOffset;
        [SerializeField] private int  quality;
        
        [SerializeField] private GameObject fluidStreamPrefab;

        [SerializeField] private bool _isPouring;
        [SerializeField] private bool _isDebbug;
        [SerializeField] private float debugCubeScale = 0.01f;

            [Space] [SerializeField] private string waterId;
      

        private Vector3 _downedPosition;
        private GameObject _instance;
        
        private void Update()
        {
            var calculatePourAngle = CalculatePourAngle();
            var pourCheck = calculatePourAngle > pourThreshold;

            _isPouring = pourCheck;
            if (_isPouring)
            {
                _downedPosition = GetDownedPos();
                pourPower = calculatePourAngle - pourThreshold/ 360 -pourThreshold ; 
                if (_instance == null)
                {
                   
                    _instance = Instantiate(fluidStreamPrefab, _downedPosition,new Quaternion());
                    try
                    {
                        _instance.GetComponent<StreamController>().parentFluid = this;
                    }
                    catch (Exception)
                    {
                        Debug.LogWarning("cant find Stream controller in stream prefab", this);
                    }
                    Physics2D.queriesHitTriggers = true;
                    var position = _instance.transform.position;
                    Physics.Raycast(position, transform.TransformDirection(Vector3.down), out var hit, Mathf.Infinity);
                    Debug.DrawRay(position, hit.distance*Vector3.down,Color.blue,1);

                    var cookStation = hit.collider.GetComponentInParent<CookStationController>();
                    Debug.Log(hit.collider.name);
                    if (cookStation != null)
                    {
                        var waterComponent = gameObject.AddComponent<IngredientController>();
                        waterComponent.idIngredient = waterId;
                        
                        cookStation.OnIngredientAdd(waterComponent);
                      
                    }
                }

                _instance.transform.position = _downedPosition;
             
            }
            else
            {
                if (_instance)
                {
                    Destroy(_instance);
                }
            }
        }

        private Vector3 GetDownedPos()
        {
            
            var circumferencePoints = GetCircumferencePoints(quality,radius);
            for (var index = 0; index < circumferencePoints.Count; index++)
            {
                var point = circumferencePoints[index];
                Transform transform1;
                var pointResult = (transform1 = transform).rotation * (Quaternion.Euler(rotOffset) * point) +
                                  transform1.up * upOffset;
                pointResult += transform.position;
                circumferencePoints[index] = pointResult;

            }
            
            var downedPoint = circumferencePoints[0];

            foreach (var point in circumferencePoints)
            {
                if (point.y < downedPoint.y)
                {
                    downedPoint = point;
                }
            }


            return downedPoint;
        }

        private float CalculatePourAngle()
        {
            return Vector3.Angle(Vector3.up, transform.up);
        }

        private void OnDrawGizmosSelected()
        {
            if (!_isDebbug) {return;}

            var circumferencePoints = GetCircumferencePoints(quality,radius);
            for (var index = 0; index < circumferencePoints.Count; index++)
            {
                var point = circumferencePoints[index];
                Transform transform1;
                var pointResult = (transform1 = transform).rotation* ( Quaternion.Euler(rotOffset) *  point) + transform1.up * upOffset;
                pointResult += transform.position;
                circumferencePoints[index] = pointResult;
                Gizmos.DrawCube( pointResult,new Vector3(debugCubeScale,debugCubeScale,debugCubeScale));
               
            }
            
            var downedPoint = circumferencePoints[0];

            foreach (var point in circumferencePoints)
            {
                if (point.y < downedPoint.y)
                {
                    downedPoint = point;
                }
            }

            _downedPosition = downedPoint;
            Gizmos.color = Color.red;
            
            Gizmos.DrawCube(_downedPosition, new Vector3(debugCubeScale, debugCubeScale, debugCubeScale));
        } // for debug
        
        List<Vector3> GetCircumferencePoints(int sides, float radius)   
        {
            List<Vector3> points = new List<Vector3>();
            float circumferenceProgressPerStep = (float)1/sides;
            float TAU = 2*Mathf.PI;
            float radianProgressPerStep = circumferenceProgressPerStep*TAU;
        
            for(int i = 0; i<sides; i++)
            {
                float currentRadian = radianProgressPerStep*i;
                var resultValue = new Vector3(Mathf.Cos(currentRadian)*radius, Mathf.Sin(currentRadian)*radius,0);
                points.Add(resultValue);
            }
            return points;
        }
    }
}