using System;
using UnityEngine;

namespace qjklw.Data
{
    [Serializable]
    public class PlayerRotateData
    {
        [field: SerializeField] public float RotationTime { get; private set; } = 0.14f;
    }
}