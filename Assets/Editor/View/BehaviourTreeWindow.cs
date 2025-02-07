using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BehaviourTree;
using BehaviourTree.BehaviourTreeSO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace qjklw.Editors
{

    public class BehaviourTreeWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
        
        public static BehaviourTreeWindow windowRoot;

        public TreeView treeView;

        // _代表快捷键，% 是 Ctrl，# 是 Shift，& 是 Alt
        [MenuItem("Tools/BehaviourTreeWindow _#&i")]
        public static void ShowExample() {
            BehaviourTreeWindow wnd = GetWindow<BehaviourTreeWindow>("BehaviourTreeWindow");
        }

        public void CreateGUI() {
            // 获取单例
            int id = BTSetting.GetSetting().TreeID;
            // 获取对象实现的获取根对象接口
            var iGetBt = EditorUtility.InstanceIDToObject(id) as IGetBT;
            
            // Debug.Log($"id: {id}   iGetBt: {iGetBt}");
            
            // 设置窗口单例
            windowRoot = this;
            
            // 获取视觉树, 转移 uxml 的根
            var root = rootVisualElement;
            m_VisualTreeAsset.CloneTree(root);

            // root.Q 查找第一个符合条件的 treeView
            treeView = root.Q<TreeView>();
            if (iGetBt?.GetRoot() == null) return;
            CreateRoot(iGetBt.GetRoot());
            
            // 调用连线
            foreach (var node in treeView.nodes.OfType<NodeView>()) {
                node.LinkLine();
            }
        }
        
        // 通过根节点创建树 (根节点要另外处理)
        public void CreateRoot(BTNodeBase rootNode) {
            if (rootNode == null) return;
            NodeView nodeView = new NodeView(rootNode);
            nodeView.SetPosition(new Rect(rootNode.Position, Vector2.one));
            treeView.AddElement(nodeView);
            treeView.NodeViews.Add(rootNode.Guid, nodeView);
            
            // 根节点另外处理

            switch (rootNode) {
                case BTComposite composite:
                    composite.ChildNodes.ForEach(CreateChild);
                    break;
                case BTPrecondition precondition:
                    CreateChild(precondition.ChildNode);
                    break;
            }   
        }

        // 递归创建子节点视图
        public void CreateChild(BTNodeBase nodeData) {
            if (nodeData == null) return;
            NodeView nodeView = new NodeView(nodeData);
            nodeView.SetPosition(new Rect(nodeData.Position, Vector2.one));
            treeView.AddElement(nodeView);
            treeView.NodeViews.Add(nodeData.Guid, nodeView);
            
            switch (nodeData) {
                case BTComposite composite:
                    composite.ChildNodes.ForEach(CreateChild);
                    break;
                case BTPrecondition precondition:
                    CreateChild(precondition.ChildNode);
                    break;
            }  
        }
    }
    
    
    // 右键菜单
    public class RightClickMenu : ScriptableObject, ISearchWindowProvider
    {
        public delegate bool SelectEntryDelegate(SearchTreeEntry searchTreeEntry, SearchWindowContext context);

        public SelectEntryDelegate OnSelectEntryHandler;
        
        // 菜单树，就是右键菜单的那种层级
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) {
            var entries = new List<SearchTreeEntry>();
            // 这个 SearchTreeGroupEntry 相当于一个 "文件夹" 也是最基本的根目录，如果没有或者类型错了就会报错
            entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
            entries = AddNodeType<BTComposite>(entries, "组合节点");
            entries = AddNodeType<BTPrecondition>(entries, "条件节点");
            entries = AddNodeType<BTAction>(entries, "行为节点");
            return entries;
        }
        
        // 这个接口实现提供了菜单树点击以后触发的逻辑
        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context) {
            if (OnSelectEntryHandler == null) {
                return false;
            }
            return OnSelectEntryHandler(searchTreeEntry, context);
        }
        
        // 通过反射获取对应的菜单数据，T 代表基类，这个函数添加所有 T 的派生类进入菜单树
        public List<SearchTreeEntry> AddNodeType<T>(List<SearchTreeEntry> entries, string pathName) {
            entries.Add(new SearchTreeGroupEntry(new GUIContent(pathName)) { level = 1 });
            List<Type> rootNodeTypes = GetDerivedClasses(typeof(T));
            foreach (var rootType in rootNodeTypes) {
                string menuName = rootType.Name;
                entries.Add(new SearchTreeEntry(new GUIContent(menuName)) { level = 2, userData = rootType });
            }
            return entries;
        }


        public static List<Type> GetDerivedClasses(Type type) {
            List<Type> derivedClasses = new List<Type>();
            // 这部分获取当前应用程序域中加载的所有程序集（Assembly）。程序集是包含类型、资源等信息的单元。
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                // 遍历每个程序集中的所有类型
                foreach (Type t in assembly.GetTypes()) {
                    // 1. 确保类型 t 是一个类（而不是接口、枚举等）。
                    // 2. 确保类型 t 不是抽象类。抽象类不能被实例化，因此不能作为派生类使用。
                    // 3. type.IsAssignableFrom(t) 这是关键部分，检查类型 t 是否是 type 的派生类。
                    if (t.IsClass && !t.IsAbstract && type.IsAssignableFrom(t)) {
                        // 添加
                        derivedClasses.Add(t);
                    }
                }
            }
            // 返回这样的类型列表
            return derivedClasses;
        }
    }
}
