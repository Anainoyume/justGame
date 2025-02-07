using System;
using System.Collections;
using System.Collections.Generic;
using qjklw.AnimationEvent;
using qjklw.CameraScript;
using qjklw.Data;
using qjklw.FSM.StateMachines;
using UnityEngine;

namespace qjklw
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerAnimationEvent))]
    public class Player : MonoBehaviour
    {
        #region Variables
        public CharacterController PlayerController { get; private set; }
        public Animator Animator { get; private set; }  
        public PlayerInput Input { get; private set; }
        public PlayerPhysics PlayerPhysics { get; private set; }
        public MainStateMachine StateMachine { get; private set; }
        
        [field: SerializeField] public PlayerSO PlayerSO { get; private set; }
        [field: SerializeField] public PlayerComboData ComboData { get; private set; }
        [field: SerializeField] public PlayerComboData RunAttackData { get; private set; }
        
        public Transform MainCameraTransform { get; private set; }
        
        [field: Header("Camera")]
        [field: SerializeField]
        public CameraRecenterUtility CameraUtility { get; private set; }
        
        #endregion
        
        
        
        #region Unity Callbacks
        private void Awake() {
            PlayerController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            Input = GetComponent<PlayerInput>();
            
            PlayerPhysics = new PlayerPhysics(this);
            StateMachine = new MainStateMachine(this);
            
            MainCameraTransform = Camera.main?.transform;
        }

        private void Start() {
            PlayerPhysics.Initialize();
            CameraUtility.Initialize(StateMachine.Data);
            
            StateMachine.ChangeState(StateMachine.PlayerIdlingState);
        }

        private void Update() {
            PlayerPhysics.Update();
            
            StateMachine.HandleInput();
            StateMachine.Update();
        }

        private void OnAnimatorMove() {
            if (StateMachine.Data.AnimatorSettingData.HandleRootMotion) {
                PlayerController.Move(StateMachine.Data.GroundData.MovementDirection * Animator.deltaPosition.magnitude);
                return;
            }
            PlayerController.Move(Animator.deltaPosition);
        }

        #endregion
        
        
        
        
    }
}