using System.Collections.Generic;
using qjklw.InspectorAttribute;
using UnityEngine;

namespace BehaviourTree.BehaviourTreeSO
{
    [CreateAssetMenu]
    public class BehaviourTreeData : ScriptableObject, IGetBT
    {
        [SerializeReference]
        public BTNodeBase rootNode;
        
        public void Tick() {
            rootNode.Tick();
        }
        
// if 预编译宏, 只有在 Unity 编辑模式才有里面的内容
#if UNITY_EDITOR
        
        [Button]
        public void OpenView() {
            BTSetting.GetSetting().TreeID = GetInstanceID();
            UnityEditor.EditorApplication.ExecuteMenuItem("Tools/BehaviourTreeWindow");
        }
        
#endif
        
        public BTNodeBase GetRoot() => rootNode;
        public void SetRoot(BTNodeBase rootData) => rootNode = rootData;
    }
    
    
    public interface IGetBT
    {
        BTNodeBase GetRoot();
        void SetRoot(BTNodeBase rootData);
    }
}