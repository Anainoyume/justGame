using qjklw.FSM.StateMachines;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState
{
    // 抽象一层总的 WalkState, 提供全过程可转移的过渡
    public class PlayerWalkState : BaseGroundState
    {
        public PlayerWalkState(MainStateMachine stateMachine) : base(stateMachine) { }



        #region Input Methods

        protected override void OnAttackStart(InputAction.CallbackContext ctx) {
            base.OnAttackStart(ctx);
            
            StateMachine.ChangeState(StateMachine.PlayerComboState);
        }

        #endregion
    }
}