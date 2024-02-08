using System;
using System.Collections.Generic;
using System.Linq;
using _main.scripts.MapSystem.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

namespace _main.scripts.MapSystem
{
    public class NodeGraph : GraphView
    {
        public NodeGraph()
        {
            this.styleSheets.Add(Resources.Load<StyleSheet>("DialogGraph"));
            
            this.SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        } // create NodeGraph
        public void Load(MapData mapData) // load/init(create node) method
        {
            var inputs = new Dictionary<string, Port>(); // inputs which can connect
            var edgeRequests = new List<(Port output, string inputId)>(); // requests for connection
            var allNode = new List<MapNode>();
            foreach (var mapNode in mapData.nodes) // get one node
            {
                var inputNodeList = DictionaryUtils.Split(mapNode.nextDataIds); // get dictionary< outputId , inputId>
                MapNode node;
                node = mapNode.sceneData == null
                    ? AddEventNode(mapNode.position)
                    : new MapNode(this,
                        mapNode.position,
                        mapNode.sceneData,
                        inputNodeList.Values.ToList());
               
                allNode.Add(node);


                for (var i = 0; i < inputNodeList.Count; i++)
                {
                    var outputs = inputNodeList.Values.ToList()[i]; // get one output
                    
                    var nextTopicId = outputs;

                    if (nextTopicId != "nothing")
                    {
                        edgeRequests.Add((node.Outputs[i], nextTopicId)); // send one edge request
                    }
                    
                }

                foreach (var Input in  node.Inputs)
                {
                    inputs[Input.name] = Input; // add inputs
                }
                
                AddElement(node); // add element
            }
            
            foreach (var (output, inputId) in edgeRequests) // execute the request
            {
                
                var mapNode = allNode.Find(t => t.Id == inputId);
                var edge = output.ConnectTo(mapNode.Inputs[0]); // create connection
                AddElement(edge); // add conection
            }
        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            foreach (var port in ports)
            {
                compatiblePorts.Add(port);
            }

            return compatiblePorts;
        } // set connectivity
        public void AddNode(SceneInOutData sceneData)
        {
            var idsList = new List<string>();
            for (int index = 0; index < sceneData.inObjId.Count; index++)
            {
               idsList.Add(Guid.NewGuid().ToString());
            } // set id to input port
           
            this.AddElement(new MapNode(this, NodeSpawnpoint(), sceneData,idsList));
        } // create new node methode 
        public MapNode AddEventNode(Vector2 position )
        {
            var idsList = new List<string>();
            idsList.Add("in");

            var mapNode = new MapNode(this, position,"Event",idsList,idsList,new List<string>());
            this.AddElement(mapNode);
            return mapNode;
        } // create new node methode 

        public MapNode AddEventNode()
        {
            return AddEventNode(NodeSpawnpoint());
        }

        private Vector2 NodeSpawnpoint()
            => -(Vector2)this.viewTransform.position + new Vector2(30, 30); // pos to spawn node
        public void Save(MapData dialog)
        {
            dialog.Clear();
            this.Query<MapNode>().ForEach(node => dialog.Add(node.Save()));
            dialog.Persist();
        }
       
    }
}