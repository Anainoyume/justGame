using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState.End
{
    public class PlayerSprintStopState : PlayerSprintState
    {
        public PlayerSprintStopState(MainStateMachine stateMachine) : base(stateMachine) { }
        
        public override StateId Id => StateId.SprintStop;



        #region IState Methods

        public override void Enter() {
            base.Enter();
            PlayAnimation("SprintStop", StateMachine.Player.PlayerSO.MovementData.SprintSpeedMultiplier);
        }

        public override void Update() {
            base.Update();
            
            RotateToTarget();
            
            if (Data.GroundData.MovementInput == Vector2.zero) {
                return;
            }
            
            if (!Data.AnimatorSettingData.SprintStopCache) {
                return;
            }

            ChangeToMoveLoopState();
        }
        
        public override void Exit() {
            base.Exit();
            Data.AnimatorSettingData.SprintStopCache = false;
        }

        #endregion
        
        
        
        #region Input Methods
        protected override void OnDashStart(InputAction.CallbackContext obj) {
            if (!Data.AnimatorSettingData.SprintStopCache) {
                return;
            }
            base.OnDashStart(obj);
        }
        #endregion
    }
}