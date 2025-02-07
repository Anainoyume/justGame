using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace qjklw.Data
{
    [Serializable]
    public class PlayerPhysicsData
    {
        [field: SerializeField]
        [field: Range(-10f, 20f)]
        public float Gravity { get; set; } = 9.8f;

        [HideInInspector]
        public Vector3 downVelocity = Vector3.zero;
    }
}