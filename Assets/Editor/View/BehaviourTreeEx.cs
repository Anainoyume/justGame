using BehaviourTree;
using UnityEditor.Experimental.GraphView;

namespace qjklw.Editors
{
    public static class BehaviourTreeEx
    {
        public static void LinkLineAddData(this Edge edge) {
            NodeView outputNode = edge.output.node as NodeView;
            NodeView inputNode = edge.input.node as NodeView;
            
            switch (outputNode.NodeData) {
                case BTComposite composite:
                    composite.ChildNodes.Add(inputNode.NodeData);
                    break;
                case BTPrecondition precondition:
                    precondition.ChildNode = inputNode.NodeData;
                    break;
            }
        }

        public static void UnLinkLineDelete(this Edge edge) {
            NodeView outputNode = edge.output.node as NodeView;
            NodeView inputNode = edge.input.node as NodeView;
            
            switch (outputNode.NodeData) {
                case BTComposite composite:
                    composite.ChildNodes.Remove(inputNode.NodeData);
                    break;
                case BTPrecondition precondition:
                    precondition.ChildNode = null;
                    break;
            }
        }
    }
}