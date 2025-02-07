using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState.End
{
    public class PlayerRunStopState : PlayerRunState
    {
        public PlayerRunStopState(MainStateMachine stateMachine) : base(stateMachine) { }

        
        
        #region IState Methods
        public override StateId Id => StateId.RunStop;
        protected override float CrossFadeTime => 0.5f;

        public override void Enter() {
            base.Enter();
            Data.AnimatorSettingData.MoveStop = true;
            PlayAnimation("RunStop", StateMachine.Player.PlayerSO.MovementData.RunSpeedMultiplier);
        }

        public override void Update() {
            base.Update();
            
            RotateToTarget();

            if (Data.GroundData.MovementInput == Vector2.zero) {
                return;
            }

            ChangeToMoveStartState();
        }

        public override void Exit() {
            base.Exit();
            
            Data.AnimatorSettingData.MoveStop = false;
        }

        #endregion
        
        
    }
}