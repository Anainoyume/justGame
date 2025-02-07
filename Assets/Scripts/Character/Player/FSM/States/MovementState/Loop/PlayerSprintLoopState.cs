using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState.Loop
{
    public class PlayerSprintLoopState : PlayerSprintState
    {
        public PlayerSprintLoopState(MainStateMachine stateMachine) : base(stateMachine) { }
        
        public override StateId Id => StateId.SprintLoop;



        #region IState Methods

        public override void Enter() {
            base.Enter();
            
            PlayAnimation("SprintLoop", StateMachine.Player.PlayerSO.MovementData.SprintSpeedMultiplier);
        }

        public override void Update() {
            base.Update();

            if (Data.GroundData.MovementInput != Vector2.zero) {
                return;
            }
            
            StateMachine.ChangeState(StateMachine.PlayerSprintStopState);
        }

        public override void Exit() {
            base.Exit();
            Data.GroundData.ShouldSprint = false;
        }

        #endregion


        #region Input Methods
        protected override void OnSprintCanceled(InputAction.CallbackContext obj) {
            base.OnSprintCanceled(obj);

            if (Data.GroundData.MovementInput == Vector2.zero) {
                return;
            }
            StateMachine.ChangeState(StateMachine.PlayerSprintStopState);
        }

        protected override void OnDashStart(InputAction.CallbackContext obj) {
            
        }

        #endregion
    }
}