using System;
using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.DashState.SlideState
{
    public class PlayerSlideFrontState : PlayerSlideBaseState
    {
        public PlayerSlideFrontState(MainStateMachine stateMachine) : base(stateMachine) { }

        #region IState Methods
        public override StateId Id => StateId.SlideFront;
        
        public override void Enter() {
            base.Enter();
            
            PlayAnimation("SlideFront");
        }

        public override void Update() {
            base.Update();

            if (!Data.AnimatorSettingData.DashFrontToLoopReady) {
                return;
            }

            if (Data.GroundData.MovementInput == Vector2.zero) {
                return;
            }
            
            Data.AnimatorSettingData.DashFrontToLoopReady = false;
            
            if (Data.GroundData.ShouldSprint) {
                StateMachine.ChangeState(StateMachine.PlayerSprintLoopState);
                return;
            }
            
            ChangeToMoveLoopState();
        }

        public override void Exit() {
            base.Exit();
            
            Data.AnimatorSettingData.DashFrontToLoopReady = false;
        }

        #endregion
        
        
        
        #region Input Methods
        protected override void OnDashStart(InputAction.CallbackContext obj) {
            if (!Data.AnimatorSettingData.DashFrontToLoopReady) {
                return;
            }

            Data.AnimatorSettingData.DashFrontToLoopReady = false;
            ChangeToSlideState();
        }
        
        protected override void OnSprintPerformed(InputAction.CallbackContext obj) {
            base.OnSprintPerformed(obj);
            
            Data.GroundData.ShouldSprint = true;
        }

        protected override void OnWalkToggle(InputAction.CallbackContext ctx) {
            
        }


        protected override void OnAttackStart(InputAction.CallbackContext ctx) {
            base.OnAttackStart(ctx);
            
            if (!Data.AnimatorSettingData.DashFrontToLoopReady) {
                return;
            }
            
            StateMachine.ChangeState(StateMachine.PlayerRunAttackState);
        }

        #endregion
        
    }
}