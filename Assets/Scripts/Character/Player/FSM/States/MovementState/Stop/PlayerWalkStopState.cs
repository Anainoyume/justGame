using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState.End
{
    public class PlayerWalkStopState : PlayerWalkState
    {
        public PlayerWalkStopState(MainStateMachine stateMachine) : base(stateMachine) { }

        #region IState Methods
        public override StateId Id => StateId.WalkStop;
        protected override float CrossFadeTime => 0.5f;
        
        public override void Enter() {
            base.Enter();
            Data.AnimatorSettingData.MoveStop = true;
            PlayAnimation("WalkStop", StateMachine.Player.PlayerSO.MovementData.WalkSpeedMultiplier);
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