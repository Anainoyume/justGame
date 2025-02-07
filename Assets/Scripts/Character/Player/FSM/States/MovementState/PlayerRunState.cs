using qjklw.FSM.StateMachines;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState
{
    public class PlayerRunState : BaseGroundState
    {
        public PlayerRunState(MainStateMachine stateMachine) : base(stateMachine) { }
        
        
        #region Input Methods

        protected override void OnAttackStart(InputAction.CallbackContext ctx) {
            base.OnAttackStart(ctx);
            
            StateMachine.ChangeState(StateMachine.PlayerComboState);
        }

        #endregion
    }
}