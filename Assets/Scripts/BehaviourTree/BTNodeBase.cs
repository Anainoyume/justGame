using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace BehaviourTree
{
    public enum BehaviourState
    {
        IDLE,
        SUCCESS, 
        FAILURE, 
        EXECUTING
    }
    
    // 抽象基类节点
    [Serializable]
    public abstract class BTNodeBase
    {
        public string Guid; // 唯一标识
        public string NodeName;
        public Vector2 Position;

        public abstract BehaviourState Tick();
    }

    // 组合抽象节点，可以有多个孩子节点
    [Serializable]
    public abstract class BTComposite : BTNodeBase
    {
        [SerializeReference]
        public List<BTNodeBase> ChildNodes = new();
    }
    
    // 断言抽象节点，只能修饰一个子节点
    [Serializable]
    public abstract class BTPrecondition : BTNodeBase
    {
        [SerializeReference]
        public BTNodeBase ChildNode;
    }
    
    // 行为节点，实现具体逻辑，而且必须是叶子节点，没有子节点
    [Serializable]
    public abstract class BTAction : BTNodeBase {}



    // 下面来实现有具体功能的节点
    
    
    
    // 顺序节点 : 失败返回, 或者全部成功 (与)
    [Serializable]
    public class BTSequence : BTComposite
    {
        private int _index;
        
        public override BehaviourState Tick() {
            var state = ChildNodes[_index].Tick();
            switch (state) {
                case BehaviourState.SUCCESS:
                    _index += 1;
                    if (_index < ChildNodes.Count) 
                        return BehaviourState.EXECUTING;
                    _index = 0;
                    return BehaviourState.SUCCESS;

                case BehaviourState.FAILURE:
                    _index = 0;
                    return BehaviourState.FAILURE;
                
                default:
                    return state;
            }
        }
    }
    
    // 选择节点: 成功返回, 或者全部失败 (或)
    [Serializable]
    public class BTSelector : BTComposite
    {
        private int _index;
        public override BehaviourState Tick() {
            var state = ChildNodes[_index].Tick();
            switch (state) {
                case BehaviourState.FAILURE:
                    _index += 1;
                    if (_index < ChildNodes.Count) 
                        return BehaviourState.EXECUTING;
                    _index = 0;
                    return BehaviourState.FAILURE;
                
                case BehaviourState.SUCCESS:
                    _index = 0;
                    return BehaviourState.SUCCESS;
                
                default:
                    return state;
            }
        }
    }
    
    
    // 装饰器节点: 延时触发
    [Serializable]
    public class Delay : BTPrecondition
    {
        public float Timer;
        private float _currentTimer;
        
        public override BehaviourState Tick() {
            _currentTimer += Time.deltaTime;
            if (_currentTimer < Timer) 
                return BehaviourState.EXECUTING;
            
            _currentTimer = 0f;
            return ChildNode.Tick();
        }
    }
    
    // 条件节点
    [Serializable]
    public class BTCondition : BTPrecondition
    {
        public bool IsActive;
        public override BehaviourState Tick() {
            return IsActive ? ChildNode.Tick() : BehaviourState.FAILURE;
        }
    }

    // 行为节点: 设置对象的活动状态
    [Serializable]
    public class SetObjectActive : BTAction
    {
        public bool IsActive;
        public GameObject SelectObject;
        public override BehaviourState Tick() {
            SelectObject.SetActive(IsActive);
            Debug.Log(NodeName + (IsActive ? "节点 启用" : "节点 禁用"));
            return BehaviourState.SUCCESS;
        }
    }
}