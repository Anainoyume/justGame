using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.DashState.SlideState
{
    public class PlayerSlideBackState : PlayerSlideBaseState
    {
        public PlayerSlideBackState(MainStateMachine stateMachine) : base(stateMachine) { }
        
        
        
        #region IState Methods
        public override StateId Id => StateId.SlideBack;
        public override void Enter() {
            base.Enter();

            // 后撤步禁止旋转
            Data.GroundData.EnableRotate = false;
            
            PlayAnimation("SlideBack");
        }

        public override void Exit() {
            base.Exit();
            Data.GroundData.EnableRotate = true;
            Data.AnimatorSettingData.DashBackAgainReady = false;
        }

        #endregion
        
        
        
        #region Input Methods
        protected override void OnDashStart(InputAction.CallbackContext obj) {
            if (!Data.AnimatorSettingData.DashBackAgainReady) {
                return;
            }

            Data.AnimatorSettingData.DashBackAgainReady = false;
            ChangeToSlideState();
        }

        protected override void OnAttackStart(InputAction.CallbackContext ctx) {
            base.OnAttackStart(ctx);
            
            if (!Data.AnimatorSettingData.DashBackAgainReady) {
                return;
            }
            
            StateMachine.ChangeState(StateMachine.PlayerComboState);
        }

        #endregion
    }
}