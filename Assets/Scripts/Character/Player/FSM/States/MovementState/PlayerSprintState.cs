using qjklw.FSM.StateMachines;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState
{
    public class PlayerSprintState : BaseGroundState
    {
        public PlayerSprintState(MainStateMachine stateMachine) : base(stateMachine) { }
        
        
        
        #region Input Methods

        protected override void OnAttackStart(InputAction.CallbackContext ctx) {
            base.OnAttackStart(ctx);
            
            StateMachine.ChangeState(StateMachine.PlayerRunAttackState);
        }

        #endregion
    }
}