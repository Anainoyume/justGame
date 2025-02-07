using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace qjklw.CameraScript
{
    public class CameraZoom : MonoBehaviour
    {
        [field: SerializeField]
        [field: Range(0.0f, 10.0f)]
        public float DefualtDistance { get; private set; } = 6f;
        
        [field: SerializeField]
        [field: Range(0.0f, 10.0f)]
        public float MaximumDistance { get; private set; } = 6f;
        
        [field: SerializeField]
        [field: Range(0.0f, 10.0f)]
        public float MinimumDistance { get; private set; } = 1f;
        
        [field: SerializeField]
        [field: Range(0.0f, 10.0f)]
        public float ZoomSensitivity { get; private set; } = 4f;
        
        [field: SerializeField]
        [field: Range(0.0f, 10.0f)]
        public float Smoothing { get; private set; } = 1f;


        private CinemachineFramingTransposer framingTransposer;
        private CinemachineInputProvider inputProvider;
        
        public float targetDistance { get; private set; }
        
        private void Awake() {
            targetDistance = DefualtDistance;   
            
            framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            inputProvider = GetComponent<CinemachineInputProvider>();
        }
        
        private void Update() {
            Zoom();
        }

        private void Zoom() {
            var zoomValue = inputProvider.GetAxisValue(2) * ZoomSensitivity;
            targetDistance = Mathf.Clamp(targetDistance + zoomValue, MinimumDistance, MaximumDistance);
            
            var currentDistance = framingTransposer.m_CameraDistance;
            if (currentDistance == targetDistance) {
                return;
            }
            
            var smoothDistance = Mathf.Lerp(currentDistance, targetDistance, Smoothing * Time.deltaTime);
            framingTransposer.m_CameraDistance = smoothDistance;
        }
    }

}
