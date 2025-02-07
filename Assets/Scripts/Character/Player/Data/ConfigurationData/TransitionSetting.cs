using System;
using System.Collections.Generic;
using qjklw.Data.AnimationData;
using UnityEngine;

namespace qjklw.Data
{
    [Serializable]
    public struct TransitionLoader
    {
        public string TransitionName;
        public TransitionData transitionSetting;
    }
    
    [Serializable]
    public class TransitionSetting
    {
        [field: SerializeField]
        public List<TransitionLoader> Transitions { get; set; }
    }
}