using qjklw.FSM.StateMachines;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState.Start
{
    public class PlayerWalkStartState : PlayerWalkState
    {
        public PlayerWalkStartState(MainStateMachine stateMachine) : base(stateMachine) { }



        #region IState Methods
        public override StateId Id => StateId.WalkStart;
        
        public override void Enter() {
            base.Enter();
            
            PlayAnimation("WalkStart", StateMachine.Player.PlayerSO.MovementData.WalkSpeedMultiplier);
        }

        public override void Update() {
            base.Update();
            
            RotateToTarget();
        }

        #endregion
        


        #region Input Methods
        protected override void OnMovementStop(InputAction.CallbackContext obj) {
            base.OnMovementStop(obj);
            
            StateMachine.ChangeState(StateMachine.PlayerWalkStopState);
        }
        #endregion
    }
}