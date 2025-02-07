using System;
using UnityEngine;

namespace qjklw.InspectorAttribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string Name;

        public ButtonAttribute() {
            Name = null;
        }
        
        public ButtonAttribute(string name) {
            Name = name;
        }
    }
}