using System;
using Cinemachine;
using qjklw.Data;
using UnityEngine;

namespace qjklw.CameraScript
{
    [Serializable]
    public class CameraRecenterUtility 
    {
        [field: SerializeField]
        public CinemachineVirtualCamera VirtualCamera { get; set; }
        private CinemachinePOV cinemachinePOV;
        private CameraZoom cameraZoom;
        
        private StateData Data { get; set; }

        [field: SerializeField] 
        [field: Range(0f, 10f)]
        public float BaseRecenteringTime { get; set; } = 1.0f;

        [field: SerializeField]
        public AnimationCurve ZoomCurve { get; set; }
        
        [field: SerializeField]
        public AnimationCurve AngleCurve { get; set; }
        
        private bool IsEnable { get; set; } = false;
        
        public void Initialize(StateData data) {
            Data = data;
            cinemachinePOV = VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
            cameraZoom = VirtualCamera.GetComponent<CameraZoom>();
        }

        public void Update() {
            if (!IsEnable) {
                return;
            }

            if (Data.GroundData.MovementInput == Vector2.zero) {
                Disable();
                return;
            }

            if (Data.GroundData.MovementInput == Vector2.up) {
                Disable();
                return;
            }
            
            if (Data.GroundData.MovementInput == Vector2.down) {
                Disable();
                return;
            }
            
            var standAngle = GetStandAngle(VirtualCamera.transform.eulerAngles.x);
            var value = ZoomCurve.Evaluate(cameraZoom.targetDistance) + AngleCurve.Evaluate(standAngle) + BaseRecenteringTime;
            // Debug.Log(value);
            cinemachinePOV.m_HorizontalRecentering.m_RecenteringTime = value;
        }

        public void Enable() {
            IsEnable = true;
            cinemachinePOV.m_HorizontalRecentering.m_enabled = true;
        }

        public void Disable() {
            IsEnable = false;
            cinemachinePOV.m_HorizontalRecentering.m_enabled = false;
        }



        #region Resuable Methods
        private float GetStandAngle(float standAngle) {
            if (standAngle >= 270f) {
                standAngle -= 360f;
            }
            return Mathf.Abs(standAngle);
        }
        #endregion
    }
}