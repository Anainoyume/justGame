using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.DashState.SlideState
{
    public class PlayerSlideBaseState : BaseGroundState
    {
        public PlayerSlideBaseState(MainStateMachine stateMachine) : base(stateMachine) { }



        #region IState Methods
        public override StateId Id => StateId.Base;
        protected override float CrossFadeTime => 0.2f;
        
        
        public override void Enter() {
            base.Enter();
            StateMachine.Data.GroundData.ShouldWalk = false;
            StateMachine.Data.AnimatorSettingData.HandleRootMotion = false;
        }

        public override void Exit() {
            base.Exit();
            
            StateMachine.Data.AnimatorSettingData.HandleRootMotion = true;
            StateMachine.Data.AnimatorSettingData.DashSignal = false;
        }

        #endregion
        
        
        
        #region Resuable Methods

        protected void ChangeToSlideState() {
            if (Data.GroundData.MovementInput == Vector2.zero) {
                StateMachine.ChangeState(StateMachine.PlayerSlideBackState);
                return;
            }
            
            StateMachine.ChangeState(StateMachine.PlayerSlideFrontState);
        }
        #endregion
    }
}