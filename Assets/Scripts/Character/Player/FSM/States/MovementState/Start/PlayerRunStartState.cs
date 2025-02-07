using qjklw.FSM.StateMachines;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState.Start
{
    public class PlayerRunStartState : PlayerRunState
    {
        public PlayerRunStartState(MainStateMachine stateMachine) : base(stateMachine) { }



        #region IState Methods
        public override StateId Id => StateId.RunStart;
        public override void Enter() {
            base.Enter();
            
            PlayAnimation("RunStart", StateMachine.Player.PlayerSO.MovementData.RunSpeedMultiplier);
        }

        public override void Update() {
            base.Update();
            
            RotateToTarget();
        }

        #endregion



        #region Input Methods
        protected override void OnMovementStop(InputAction.CallbackContext obj) {
            base.OnMovementStop(obj);
            
            StateMachine.ChangeState(StateMachine.PlayerRunStopState);
        }
        #endregion
    }
}