using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace _main.scripts.MapSystem.Nodes
{
    [Serializable]
    public class MapNode : Node
    {
        public SceneInOutData sceneInOutData;
        public string Id;
        protected NodeGraph _parent;
        
        public List<Port> Inputs;
        public List<Port> Outputs;
        public Port Action;

        public List<string> PortsIds;
        public TextField TextField;


        protected Port MakePort(Direction direction, Port.Capacity capacity = Port.Capacity.Single)
        {
            return MakePort<bool>(direction, capacity);
        } //create port
        protected Port MakePort<T>(Direction direction, Port.Capacity capacity = Port.Capacity.Single)
        {
            return this.InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(T));
        } //create type port
        // public MapNode(NodeGraph parent, Vector2 position,List<string> inPort,List<string> outPort ) : this(parent, position, Guid.NewGuid().ToString(),inPort,outPort)
        // {
        // } // auto id
      
        public MapNode(NodeGraph parent, Vector2 position,SceneInOutData data, List<string> portIds) : this(parent, position, data.sceneName,portIds,data.inObjId,data.outObjId)
        {
            sceneInOutData = data;
        } // data safe
        public MapNode(NodeGraph parent, Vector2 position, string id,List<string> portIds, List<string> inPorts,List<string> outPorts)
        {
            PortsIds = portIds;
            _parent = parent;
            this.title = "Stage";
            this.Id = id;

            this.SetPosition(new Rect(position.x, position.y, 100, 200));
            
            var color = new Color(0.58f, 0.19f, 0f);
            this.title = id;
            this.titleContainer.style.backgroundColor = new StyleColor(color); // name
            
            Inputs = new List<Port>();
            
            for (var index = 0; index < inPorts.Count; index++)
            {
                var inPort = inPorts[index];
                var Input = this.MakePort(Direction.Input);
                Input.portName = inPort;
                Input.name = portIds[index];
                this.inputContainer.Add(Input); // input port
                Inputs.Add(Input);
            }

            if (outPorts.Count != 0)
            {
                Outputs = new List<Port>();
                foreach (var outPort in outPorts)
                {
                    var Output = this.MakePort(Direction.Output, Port.Capacity.Multi);
                    Output.portName = outPort;
                    this.inputContainer.Add(Output); // input port
                    Outputs.Add(Output);
                }
               
            }
            else
            {
                var textField = new TextField();
                this.mainContainer.Add(textField);
                TextField = textField;
                
                color = new Color(0.13f, 0.11f, 0.25f);
                this.titleContainer.style.backgroundColor = new StyleColor(new Color(0.09f, 0.05f, 0.25f)); // name
            }

          

            this.mainContainer.style.backgroundColor = new StyleColor(color); // set color
            // Action = this.MakePort<string>(Direction.Output);
            // Action.portName = "Action";
            // this.outputContainer.Add(Action); // output port with type
            
            

            this.RefreshExpandedState();
            this.RefreshPorts();
            
        } // create node
       
        
        
        
        public NodeData Save()
        {
            
            var topic = new NodeData
            {
                id = this.Id,
                position = this.GetPosition().position,
                sceneData = sceneInOutData
            };
            if (TextField != null)
            {
                topic.eventText = TextField.value;
            }

            Debug.Log("start save");


            topic.portIds = PortsIds;
           topic.nextDataIds = DictionaryUtils.Combine(GetOutputs());
            
            return topic;
        }
        public Dictionary<string ,string> GetOutputs()
        {
            var returnValue = new Dictionary<string ,string>();
            if (Outputs == null)
            {
                return returnValue;
            }
            foreach (var Output in Outputs)
            {
                var list = "nothing";
                foreach (var connection in Output.connections)
                {
                    var node = (MapNode)connection.input.node;
                    list =  node.Id;
                }
                returnValue.Add(Output.portName,list);
            }
            
            return returnValue;
        }
    }
}