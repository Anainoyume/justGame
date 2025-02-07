using System;
using System.Collections.Generic;
using qjklw.FSM.StateMachines;
using qjklw.FSM.States;
using UnityEditor.Animations;
using UnityEngine;

namespace qjklw.Data.AnimationData
{
    [Serializable]
    public struct TransitionData
    {
        public float CrossFadeTime;
        public float OffsetTime;

        public TransitionData(float crossFadeTime, float offsetTime) {
            CrossFadeTime = crossFadeTime;
            OffsetTime = offsetTime;
        }
    }
    
    [Serializable]
    public class AnimatorSettingData
    {
        public bool HandleRootMotion { get; set; } = true;
        
        public bool DashSignal { get; set; } = false;
        
        public bool DashFrontToLoopReady { get; set; } = false;
        
        public bool DashBackAgainReady { get; set; } = false;

        public bool SprintStopCache { get; set; } = false;
        
        public bool MoveStop { get; set; } = false;
        
        public bool ComboReady { get; set; } = false;
        
        public Dictionary<string, TransitionData> Transitions { get; set; } = new();

        public AnimatorSettingData(MainStateMachine stateMachine) {
            foreach (var item in stateMachine.Player.PlayerSO.TransitionSetting.Transitions) {
                Transitions.Add(item.TransitionName, item.transitionSetting);
            }
        }
        
        public TransitionData? GetTransitionData(StateId a, StateId b) {
            var keyStr = $"{a.ToString()}_{b.ToString()}";
            if (Transitions.TryGetValue(keyStr, out var data)) {
                return data;
            }   
            return null;
        }
    }
}