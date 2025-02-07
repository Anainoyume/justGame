using BehaviourTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace qjklw.Editors
{
    public class NodeView : Node
    {
        public BTNodeBase NodeData;

        public Port InputPort;
        public Port OutputPort;
        
        public NodeView(BTNodeBase nodeData) {
            NodeData = nodeData;
            
            InitNodeView(nodeData);
        }

        public void InitNodeView(BTNodeBase nodeData) {
            title = nodeData.NodeName;
            
            // 创建节点端口
            InputPort = +this;
            inputContainer.Add(InputPort);

            // 行为节点没有 output
            if (nodeData is BTAction) return;
            
            OutputPort = this - (nodeData is BTPrecondition);
            outputContainer.Add(OutputPort);
        }
        
        
        // 连线
        public void LinkLine() {
            TreeView graphView = BehaviourTreeWindow.windowRoot.treeView;
            switch (NodeData) {
                case BTComposite composite:
                    composite.ChildNodes.ForEach(n => {
                        graphView.AddElement(PortLink(OutputPort, graphView.NodeViews[n.Guid].InputPort));
                    });
                    break;
                
                case BTPrecondition precondition:
                    graphView.AddElement(PortLink(OutputPort, graphView.NodeViews[precondition.ChildNode.Guid].InputPort));
                    break;
            }
        }
        
        
        // 连接
        public static Edge PortLink(Port p1, Port p2) {
            var tempEdge = new Edge {
                output = p1,
                input = p2
            };
            
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            return tempEdge;
        }


        public override void SetPosition(Rect newPos) {
            base.SetPosition(newPos);
            NodeData.Position = new Vector2(newPos.xMin, newPos.yMin);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
            evt.menu.AppendAction("Set Root", SetRoot);
        }

        private void SetRoot(DropdownMenuAction obj) => BTSetting.GetSetting().SetRoot(NodeData);
        

        // Input
        public static Port operator +(NodeView view) {
            Port port = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, 
                Port.Capacity.Single, typeof(NodeView));
            return port;
        }
        
        // Output
        public static Port operator -(NodeView view, bool isSingle) {
            Port port = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, 
                isSingle ? Port.Capacity.Single : Port.Capacity.Multi, typeof(NodeView));
            return port;
        }
    }
}