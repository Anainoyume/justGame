using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using qjklw.InspectorAttribute;
using UnityEditor;
using UnityEngine;

namespace qjklw.InspectorEx
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class CustomInspectorMonoEditor : Editor
    {
        private List<MethodButtonInfo> methods;
        private void OnEnable() {
            methods = target.GetType().GetMethods()
                            .Where(m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Any())  
                            .Select(m => new MethodButtonInfo {
                                method = m,  
                                buttonName = m.GetCustomAttributes(typeof(ButtonAttribute), true)
                                              .Cast<ButtonAttribute>()
                                              .FirstOrDefault()?.Name ?? m.Name
                            })
                            .ToList();
        }

        public override void OnInspectorGUI() {
            // 其他保持不变
            base.OnInspectorGUI();
            
            // 添加函数按钮
            DrawMethodsButton();
        }

        private void DrawMethodsButton() {
            foreach (var info in methods) {
                if (GUILayout.Button(info.buttonName)) {
                    info.method.Invoke(target, null);
                }
            }
        }
    }
    
    
    [CustomEditor(typeof(ScriptableObject), true)]
    public class CustomInspectorSOEditor : Editor
    {
        private List<MethodButtonInfo> methods;
        private void OnEnable() {
            methods = target.GetType().GetMethods()
                            .Where(m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Any())  
                            .Select(m => new MethodButtonInfo {
                                method = m,  
                                buttonName = m.GetCustomAttributes(typeof(ButtonAttribute), true)
                                              .Cast<ButtonAttribute>()
                                              .FirstOrDefault()?.Name ?? m.Name
                            })
                            .ToList();
        }

        public override void OnInspectorGUI() {
            // 其他保持不变
            base.OnInspectorGUI();
            
            // 添加函数按钮
            DrawMethodsButton();
        }

        private void DrawMethodsButton() {
            foreach (var info in methods) {
                if (GUILayout.Button(info.buttonName)) {
                    info.method.Invoke(target, null);
                }
            }
        }
    }
    
    public class MethodButtonInfo {
        public MethodInfo method;
        public string buttonName;
    }
}