using System.Collections.Generic;
using qjklw.CustomType;
using qjklw.Data;
using qjklw.Data.AnimationData;
using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace qjklw.FSM.States
{
    /// <summary>
    /// 基础的地面状态类, 包含最基本的移动，旋转操作，其他地面状态类均继承该类
    /// </summary>
    public class BaseGroundState : IState
    {
        #region Variables
        public MainStateMachine StateMachine { get; set; }
        protected StateData Data { get; set; }

        protected virtual float CrossFadeTime => 0.14f;
        protected virtual float OffsetTime => 0.0f;
        #endregion

        
        
        public BaseGroundState(MainStateMachine stateMachine) {
            StateMachine = stateMachine;
            Data = stateMachine.Data;
            
            IntializeData();
        }
        
        
        
        #region IState implementation
        public virtual void Enter() {
            Debug.Log("当前状态为：" + GetType().Name);
            AddInputCallBackEvents();
        }
        public virtual void Exit() {
            RemoveInputCallBackEvents();
        }
        public virtual void Update() {
            if (Data.GroundData.EnableRotate) {
                Rotate();
            }
            
            StateMachine.Player.CameraUtility.Update();
        }

        public virtual void FixedUpdate() {
            
        }
        public virtual void HandleInput() {
            StateMachine.Data.GroundData.MovementInput = StateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        public virtual StateId Id => StateId.Base;

        #endregion


        
        #region CallBack Methods
        protected virtual void AddInputCallBackEvents() {
            StateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggle;
            
            StateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStart;
            StateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
            StateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementStop;
            
            StateMachine.Player.Input.PlayerActions.Dash.started += OnDashStart;
            
            StateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
            StateMachine.Player.Input.PlayerActions.Sprint.canceled += OnSprintCanceled;

            StateMachine.Player.Input.PlayerActions.Attack.started += OnAttackStart;
        }

        

        protected virtual void RemoveInputCallBackEvents() {
            StateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggle;
            
            StateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStart;
            StateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
            StateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementStop;
            
            StateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStart;
            
            StateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerformed;
            StateMachine.Player.Input.PlayerActions.Sprint.canceled -= OnSprintCanceled;
            
            StateMachine.Player.Input.PlayerActions.Attack.started -= OnAttackStart;
        }
        #endregion



        #region Input Methods
        protected virtual void OnAttackStart(InputAction.CallbackContext ctx) {
            
        }
        private void OnMovementPerformed(InputAction.CallbackContext ctx) {
            StateMachine.Player.CameraUtility.Enable();
        }
        protected virtual void OnWalkToggle(InputAction.CallbackContext ctx) {
            Data.GroundData.ShouldWalk = !Data.GroundData.ShouldWalk;
            Debug.Log(Data.GroundData.ShouldWalk ? "切换到步行" : "切换到奔跑");
        }
        protected virtual void OnMovementStart(InputAction.CallbackContext ctx) {
            
        }
        protected virtual void OnMovementStop(InputAction.CallbackContext ctx) {
            StateMachine.Player.CameraUtility.Disable();
        }
        protected virtual void OnDashStart(InputAction.CallbackContext ctx) {
            Data.AnimatorSettingData.DashSignal = true;
            if (Data.GroundData.MovementInput == Vector2.zero) {
                StateMachine.ChangeState(StateMachine.PlayerSlideBackState);
                return;
            }
            
            StateMachine.ChangeState(StateMachine.PlayerSlideFrontState);
        }
        protected virtual void OnSprintCanceled(InputAction.CallbackContext ctx) {
            
        }

        protected virtual void OnSprintPerformed(InputAction.CallbackContext ctx) {
            
        }
        #endregion
        
        

        #region Main Methods
        private void IntializeData() {
            Data.GroundData.RotationTime = StateMachine.Player.PlayerSO.RotateData.RotationTime;
        }
        private void Rotate() {
            if (Data.GroundData.MovementInput == Vector2.zero) {
                return;
            }

            Vector3 movementInput = GetVector3MovementInput();
            float targetAngle = UpdateMovementAngle(movementInput);
            
            // 加上向心力
            Data.GroundData.MovementDirection = GetMovementDirection(targetAngle);
            
            GetCentripetalDirection();

            RotateToTarget();
        }
        


        private float UpdateMovementAngle(Vector3 movementInput, bool ConsiderCamera = true) {
            float targetAngle = GetInputAngle(movementInput);
            if (ConsiderCamera) {
                targetAngle = AddCameraAngle(targetAngle);
            }

            if (targetAngle != Data.GroundData.TargetRotationAngle) {
                UpdateRotationData(targetAngle);
            }
            
            return targetAngle; 
        }

        private void UpdateRotationData(float targetAngle) {
            Data.GroundData.TargetRotationAngle = targetAngle;
            Data.GroundData.RotationPassTime = 0f;
        }

        private float AddCameraAngle(float angle) {
            angle += StateMachine.Player.MainCameraTransform.eulerAngles.y;
            if (angle > 360.0f) angle -= 360.0f;
            return angle;
        }

        private float GetInputAngle(Vector3 movementInput) {
            float inputAngle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg;
            if (inputAngle < 0) inputAngle += 360.0f;
            return inputAngle;
        }
        #endregion
        
        
        
        #region Resuable Methods
        protected void RotateToTarget() {
            float currentAngle = StateMachine.Player.transform.eulerAngles.y;
            if (Data.GroundData.TargetRotationAngle == currentAngle) {
                return;
            }

            float smoothAngle = 
                Mathf.SmoothDampAngle(currentAngle, 
                    Data.GroundData.TargetRotationAngle,
                    ref Data.GroundData.RotationVelocityRef, 
                    Data.GroundData.RotationTime - Data.GroundData.RotationPassTime);

            Data.GroundData.RotationPassTime += Time.deltaTime;
            StateMachine.Player.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }
        protected void GetCentripetalDirection() {
            var cameraPosition = new Vector3(StateMachine.Player.MainCameraTransform.position.x, 0, StateMachine.Player.MainCameraTransform.position.z);
            var playerPosition = new Vector3(StateMachine.Player.transform.position.x, 0, StateMachine.Player.transform.position.z);
            Data.GroundData.CentripetalDirection = playerPosition - cameraPosition;
            Data.GroundData.CentripetalDirection.Normalize();
        }
        protected Vector3 GetVector3MovementInput() {
            return new Vector3(StateMachine.Data.GroundData.MovementInput.x, 0, StateMachine.Data.GroundData.MovementInput.y);
        }
        protected Vector3 GetMovementDirection(float targetAngle) {
            return Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
        }
        
        protected virtual void ChangeToMoveStartState() {
            if (Data.GroundData.ShouldWalk) {  
                StateMachine.ChangeState(StateMachine.PlayerWalkStartState);
                return;
            }
            StateMachine.ChangeState(StateMachine.PlayerRunStartState);
        }
        
        protected virtual void ChangeToMoveLoopState() {
            if (Data.GroundData.ShouldWalk) {  
                StateMachine.ChangeState(StateMachine.PlayerWalkLoopState);
                return;
            }
            StateMachine.ChangeState(StateMachine.PlayerRunLoopState);
        }

        protected void PlayAnimation(string name, float playSpeed = 1.0f) {
            StateMachine.Player.Animator.SetFloat("SpeedModifier", playSpeed);
            var option = StateMachine.Data.AnimatorSettingData.GetTransitionData(Id, StateMachine.lastStateId) ?? new TransitionData(CrossFadeTime, OffsetTime);
            StateMachine.Player.Animator.CrossFade(name, option.CrossFadeTime, 0, option.OffsetTime);
        }
        #endregion
    }
}