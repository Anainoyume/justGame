using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.MovementState.Loop
{
    public class PlayerWalkLoopState : PlayerWalkState
    {
        public PlayerWalkLoopState(MainStateMachine stateMachine) : base(stateMachine) { }



        #region IState Methods
        public override StateId Id => StateId.WalkLoop;

        public override void Enter() {
            base.Enter();
            
            PlayAnimation("WalkLoop", StateMachine.Player.PlayerSO.MovementData.WalkSpeedMultiplier);
        }

        public override void Update() {
            base.Update();
            
            if (Data.GroundData.MovementInput != Vector2.zero) {

                if (!Data.GroundData.ShouldWalk) {
                    StateMachine.ChangeState(StateMachine.PlayerRunLoopState);
                }
                
                return;
            }
            
            StateMachine.ChangeState(StateMachine.PlayerWalkStopState);
        }
        #endregion
        
        
    }
}