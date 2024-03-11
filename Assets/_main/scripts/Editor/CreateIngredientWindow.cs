using System;
using System.Collections.Generic;
using _main.scripts.MapSystem;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace _main.scripts.Editor
{
    public class CreateIngredientWindow : OdinEditorWindow
    {
        [MenuItem("My Game/Some Window")]
        private static void OpenWindow()
        {
            GetWindow<CreateIngredientWindow>().Show();
        }

        
        [HorizontalGroup("Split",0.5f,LabelWidth = 20f)]
        [BoxGroup("Split/Left")]
        public List<Mesh> meshes;
        [BoxGroup("Split/Right")]
        public List<string> names;
        
        public GameObject ingredientTemplate;
        
        [Button(ButtonSizes.Large)]
        public void CreateIngredientPrefab()
        {
            for (var index = 0; index < meshes.Count; index++)
            {
                var mesh = meshes[index];
                var s = names[index];
                var instance = Instantiate(ingredientTemplate);
                instance.name = s;
                instance.GetComponent<IngredientController>().idIngredient = s;
                instance.transform.GetChild(0).GetComponent<MeshFilter>().mesh = mesh;
                instance.transform.GetChild(0).GetComponent<MeshCollider>().sharedMesh = mesh;
                
                var prefabPath = $"Assets/Data/ingredients/{s}.prefab";
                var prefabAsset=  PrefabUtility.SaveAsPrefabAsset(instance,prefabPath);
                
                var dataInstance = CreateInstance<SceneInOutData>();
                dataInstance.sceneName = s;
                dataInstance.prefab = prefabAsset;
                dataInstance.outObjId = new List<string>();
                dataInstance.outObjId.Add("boil");
                
                var path = $"Assets/Data/ingredients/{s}.asset";
                AssetDatabase.CreateAsset(dataInstance, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = dataInstance;
            }
        }

        public List<GameObject> prefabs;
        public GameObject childPrefab;
        
        [Button(ButtonSizes.Large)]
        public void CreateChild()
        {
            foreach (var prefab in prefabs)
            {
                Instantiate(childPrefab, prefab.transform);
            }
        }
    }
[Serializable]
    public class IngredientData
    {
        public Mesh mesh;
        public string name;
    }
}