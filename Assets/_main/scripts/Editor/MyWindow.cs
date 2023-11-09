using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using _main.scripts.ConnectSystem;



namespace _main.scripts.Editor
{
    public class MyWindow : EditorWindow
    {
        public GameObject[] someThings;
        public GameObject _prefab;
        private GameObject _visual;
        private GameObject _conPos;
        private GameObject _conNeg;
        
        [MenuItem ("Window/My Window")]

        public static void  ShowWindow () {
            EditorWindow.GetWindow(typeof(MyWindow));
        }
        private void OnGUI()
        {
            GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
            _prefab = (GameObject)EditorGUILayout.ObjectField("Example GO", _prefab, typeof(GameObject), true);
            _visual = (GameObject)EditorGUILayout.ObjectField("_visual", _visual, typeof(GameObject), true);
            _conPos = (GameObject)EditorGUILayout.ObjectField("_conPos", _conPos, typeof(GameObject), true);
            _conNeg = (GameObject)EditorGUILayout.ObjectField("_conNeg", _conNeg, typeof(GameObject), true);
            
             ScriptableObject scriptableObj = this;
             SerializedObject serialObj = new SerializedObject (scriptableObj);
             SerializedProperty serialProp = serialObj.FindProperty ("someThings");
              
             EditorGUILayout.PropertyField (serialProp, true);
             serialObj.ApplyModifiedProperties ();
            
            if(GUILayout.Button("Proceed"))
            {
                CreateDetail();
            }
        }

        private void CreateDetail()
        {
        foreach(GameObject visuall in someThings ){
        
            var workPrefab = Instantiate(_prefab);
            var detail = workPrefab.GetComponent<Detail>();
            
            var visual = Instantiate(visuall, workPrefab.transform);
            detail.visual = visual;

            detail.id = visuall.name;

            var childPos = new List<Transform>();
            var childNeg = new List<Transform>();

            foreach (Transform child in visual.transform)
            {
                if (child.name[0] == "+"[0])
                {
                    childPos.Add(child);
                    var i =  Instantiate(_conPos, child.position, child.rotation,workPrefab.transform.GetChild(0).transform);

                }
                else
                {
                    childNeg.Add(child);
                    var i =Instantiate(_conNeg,child.position,child.rotation, workPrefab.transform.GetChild(1).transform);
                    i.GetComponent<Connector>().detail = detail;
                    

                }
            }
            }
            
            


        }
        
    }
}
