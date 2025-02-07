using System;
using System.Collections.Generic;
using System.Linq;
using BehaviourTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace qjklw.Editors
{
    public class TreeView : GraphView
    {
        public Dictionary<string, NodeView> NodeViews = new Dictionary<string, NodeView>();
        
        public new class UxmlFactory : UxmlFactory<TreeView, UxmlTraits> {}

        public TreeView() {
            Insert(0, new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/View/BehaviourTreeWindow.uss"));

            GraphViewMenu();

            // 试图改变的事件
            graphViewChanged += OnGraphViewChanged;
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange gvc) {
            // 线条改变的触发
            if (gvc.edgesToCreate != null) {
                gvc.edgesToCreate.ForEach(edge => {
                    edge.LinkLineAddData();
                });
            }

            if (gvc.elementsToRemove != null) {
                gvc.elementsToRemove.ForEach(ele => {
                    if (ele is Edge edge) {
                        edge.UnLinkLineDelete();
                    }
                });
            }

            return gvc;
        }

        // 右键菜单方法
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
            base.BuildContextualMenu(evt);
            // evt.menu.AppendAction("Create Node", CreateNode);
        }

        private void CreateNode(Type type, Vector2 position) {
            // 创建节点数据
            BTNodeBase nodeData = Activator.CreateInstance(type) as BTNodeBase;
            nodeData.NodeName = type.Name;  // 名字
            nodeData.Guid = Guid.NewGuid().ToString();  // 唯一标识符
            
            // 节点试图
            NodeView node = new NodeView(nodeData);
            node.SetPosition(new Rect(position, Vector2.one));
            
            NodeViews.Add(nodeData.Guid, node);
            AddElement(node);
        }
        
        private void GraphViewMenu() {
            var menuWindowProvider = ScriptableObject.CreateInstance<RightClickMenu>();
            // 在菜单选择以后触发的事件
            menuWindowProvider.OnSelectEntryHandler = OnMenuSelectEntry;

            // 打开搜索框 (菜单树)
            nodeCreationRequest += context => {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), menuWindowProvider);
            };
        }
        
        // 覆写 GetCompatiblePorts 定义连接规则，返回的 List<Port> 是所有能连的端口
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
            // 通过 Linq 筛选哪些能连：
            // 1. direction 其实是 "input" 和 "output"，首先两个端口必须类型不一样才能连接
            // 2. 节点不能自己连自己 即 node
            return ports.Where(endPorts => 
                            endPorts.direction != startPort.direction && endPorts.node != startPort.node)
                        .ToList();
        }

        
        // 创建节点的时候计算位置
        private bool OnMenuSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context) {
            // 拿到窗口的单例
            var windowRoot = BehaviourTreeWindow.windowRoot.rootVisualElement;
            
            // 获取节点应该生成的坐标
            // windowRoot.parent 是 EditorWindow 所依附的窗口空间, 如果是独立窗口那 parent 就是整个 Unity 编辑器窗口
            // 即左上角为 (0,0)
            // context.screenMousePosition 是以整个电脑屏幕的坐标为基准，屏幕左上角为 (0,0)
            // BehaviourTreeWindow.windowRoot.position.position 是 EditorWindow 窗口的左上角
            // 最后的 graphMoustPosition 是把 "鼠标相对于EditorWindow的世界坐标" 转为了 TreeView 的相对坐标
            var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent,
                context.screenMousePosition - BehaviourTreeWindow.windowRoot.position.position);

            var graphMoustPosition = contentViewContainer.WorldToLocal(windowMousePosition);
            
            
            CreateNode((Type)searchTreeEntry.userData, graphMoustPosition);
            
            return true;
        }
    }

}