using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState.Loop
{
    public class PlayerRunLoopState : PlayerRunState
    {
        public PlayerRunLoopState(MainStateMachine stateMachine) : base(stateMachine) { }


        
        #region IState Methods
        public override StateId Id => StateId.RunLoop;
        public override void Enter() {
            base.Enter();
            
            PlayAnimation("RunLoop", StateMachine.Player.PlayerSO.MovementData.RunSpeedMultiplier);
        }

        public override void Update() {
            base.Update();

            if (Data.GroundData.MovementInput != Vector2.zero) {

                if (Data.GroundData.ShouldWalk) {
                    StateMachine.ChangeState(StateMachine.PlayerWalkLoopState);
                }
                
                return;
            }
            
            StateMachine.ChangeState(StateMachine.PlayerRunStopState);
        }

        #endregion
        
        
        
        
    }
}