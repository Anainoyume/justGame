using BehaviourTree.BehaviourTreeSO;
using UnityEditor;
using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class BTSetting : ScriptableObject
    {
        public int TreeID;
        
        public static BTSetting GetSetting() {
            return Resources.Load<BTSetting>("BTSetting");
        }
        
        
#if UNITY_EDITOR
        public IGetBT GetTree() => EditorUtility.InstanceIDToObject(TreeID) as IGetBT;
        public void SetRoot(BTNodeBase rootNode) => GetTree().SetRoot(rootNode);
#endif
    }
}