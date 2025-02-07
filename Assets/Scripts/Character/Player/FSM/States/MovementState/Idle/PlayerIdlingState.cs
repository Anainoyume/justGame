using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States
{
    public class PlayerIdlingState : BaseGroundState
    {
        public PlayerIdlingState(MainStateMachine stateMachine) : base(stateMachine) { }



        #region IState Methods
        public override StateId Id => StateId.Idle;
        protected override float CrossFadeTime => 0.23f;

        public override void Enter() {
            base.Enter();
            
            PlayAnimation("Idle");
        }

        public override void Update() {
            base.Update();

            RotateToTarget();

            if (Data.GroundData.MovementInput == Vector2.zero) {
                return;
            }

            ChangeToMoveStartState();
        }
        #endregion
        
        
        
        #region Input Methods

        protected override void OnAttackStart(InputAction.CallbackContext ctx) {
            base.OnAttackStart(ctx);
            
            StateMachine.ChangeState(StateMachine.PlayerComboState);
        }

        #endregion
    }
}