using System;
using UnityEngine;

namespace qjklw.Data
{
    [Serializable]
    public class PlayerMovementData
    {
        [field: SerializeField]
        [field: Range(1f, 10f)]
        public float WalkSpeedMultiplier { get; set; } = 1f;

        [field: SerializeField]
        [field: Range(1f, 10f)]
        public float RunSpeedMultiplier { get; set; } = 1f;

        [field: SerializeField]
        [field: Range(1f, 10f)]
        public float SprintSpeedMultiplier { get; set; } = 1f;
        
        [field: SerializeField]
        public float CentripetalVelocity { get; set; } = 0f;
    }
}