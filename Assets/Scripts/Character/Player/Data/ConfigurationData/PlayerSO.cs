using System;
using System.Collections.Generic;
using qjklw.Data.AnimationData;
using UnityEngine;

namespace qjklw.Data
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player")]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField]
        public PlayerPhysicsData PhysicsData { get; set; }
        
        [field: SerializeField]
        public PlayerRotateData RotateData { get; set; }
        
        [field: SerializeField]
        public PlayerMovementData MovementData { get; set; }
        
        [field: SerializeField]
        public TransitionSetting TransitionSetting { get; set; }
    }
}