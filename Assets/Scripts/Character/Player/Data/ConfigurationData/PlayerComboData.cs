using System;
using System.Collections.Generic;
using UnityEngine;

namespace qjklw.Data
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/ComboData")]
    public class PlayerComboData : ScriptableObject
    {
        [field: SerializeField, Header("连招表")] 
        public List<ComboData> m_comboList;
    }

    [Serializable]
    public struct ComboData
    {
        public string m_comboAnimationName;
        public float m_damage;
        public bool m_allowRotate;
    }
}