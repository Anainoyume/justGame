using System;
using qjklw;
using qjklw.FSM.States;
using UnityEngine;

namespace DebugTool.TestScripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class TestBox : MonoBehaviour
    {
        private PlayerInput Input;
        private Transform cameraTransform;

        private void Awake() {
            Input = GetComponent<PlayerInput>();
            cameraTransform = Camera.main.transform;
        }

        private void Start() {
            
        }

        private void Update() {

        }
        
    }
}