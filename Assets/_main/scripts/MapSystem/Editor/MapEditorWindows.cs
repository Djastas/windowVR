using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace _main.scripts.MapSystem
{
    public class MapEditorWindows : OdinEditorWindow
    {
        private MapData _targetObject; // now edit data 
        private NodeGraph _graphView; // created graph 
        private List<SceneInOutData> _currentSceneInOutData;
        private ObjectField sceneDataField;

        public List<SceneInOutData> data;

        public void Init(MapData targetObject)
        {
            var wnd = GetWindow<MapEditorWindows>();
            // wnd.titleContent = new GUIContent(targetObject.name); // set tittle to data name
            
            _targetObject = targetObject;
            _graphView.Load(_targetObject); // init graph/load graph
        }

        protected override void OnEnable()
        {
            MakeGraphView();
            MakeToolbar();
            base.OnEnable();
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

             sceneDataField = new ObjectField
            {
                objectType = typeof(SceneInOutData)
            };
            toolbar.Add(sceneDataField);

            
            var addAnswerButton = new Button(() => _graphView.AddNode((SceneInOutData) sceneDataField.value));
            addAnswerButton.text = "SceneData Node";
            toolbar.Add(addAnswerButton);
            
            var textField = new TextField();
            toolbar.Add(textField);
            
            var addEventButton = new Button(() => _graphView.AddEventNode(textField.value));
            addEventButton.text = "Add eventNode";
            toolbar.Add(addEventButton);
            
            
            var initButton = new Button(() => _graphView.Save(_targetObject));
            initButton.text = "Save";
            toolbar.Add(initButton);
            
            

            rootVisualElement.Add(toolbar);
        }

      
        
    }
}