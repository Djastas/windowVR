using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _main.scripts.MapSystem
{
    public class MapEditorWindows : EditorWindow
    {
        private MapData _targetObject; // now edit data 
        private NodeGraph _graphView; // created graph 
        private SceneInOutData _currentSceneInOutData;
        
        public void Init(MapData targetObject)
        {
            var wnd = GetWindow<MapEditorWindows>();
            wnd.titleContent = new GUIContent(targetObject.name); // set tittle to data name
            
            _targetObject = targetObject;
            _graphView.Load(_targetObject); // init graph/load graph
        }
        
        private void OnEnable()
        {
            MakeGraphView();
            MakeToolbar();
        }
        private void MakeGraphView()
        {
            _graphView = new NodeGraph()
            {
                name = "Dialog Graph ff",
            };
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }
        
        private void MakeToolbar()
        {

            var toolbar = new Toolbar();

            var sceneDataField = new ObjectField
            {
                objectType = typeof(SceneInOutData)
            };
            toolbar.Add(sceneDataField);
            
            
            
            var addAnswerButton = new Button(() => _graphView.AddNode((SceneInOutData)sceneDataField.value));
            addAnswerButton.text = "Add Answer";
            toolbar.Add(addAnswerButton);
            
            var addEventButton = new Button(() => _graphView.AddEventNode());
            addEventButton.text = "Add eventNode";
            toolbar.Add(addEventButton);
            
            
            var initButton = new Button(() => _graphView.Save(_targetObject));
            initButton.text = "Save";
            toolbar.Add(initButton);
            
            

            rootVisualElement.Add(toolbar);
        }
        
    }
}