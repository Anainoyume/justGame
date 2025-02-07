using UnityEngine;

namespace qjklw.AnimationEvent
{
    public class PlayerAnimationEvent : MonoBehaviour
    {
        private Player player;
        private void Awake() {
            player = GetComponent<Player>();
        }

        #region Animation Event Methods
        private void OnChangeStateToWalkLoop() {
            player.StateMachine.ChangeState(player.StateMachine.PlayerWalkLoopState);   
        }
        private void OnChangeStateToRunLoop() {
            player.StateMachine.ChangeState(player.StateMachine.PlayerRunLoopState);   
        }
        private void OnChangeStateToIdle() {
            player.StateMachine.ChangeState(player.StateMachine.PlayerIdlingState);
        }

        private void OnChangeStateToMoveLoop() {
            // Debug.Log("OnChangeStateToMoveLoop 调用");
            
            if (player.StateMachine.Data.AnimatorSettingData.MoveStop) {
                return;
            }
                
            if (player.StateMachine.Data.AnimatorSettingData.DashSignal) {
                player.StateMachine.ChangeState(player.StateMachine.PlayerSlideFrontState);
                return;
            }
            
            if (player.StateMachine.Data.GroundData.ShouldWalk) {
                player.StateMachine.ChangeState(player.StateMachine.PlayerWalkLoopState);
                return;
            }
            player.StateMachine.ChangeState(player.StateMachine.PlayerRunLoopState);   
        }

        private void OnChangeStateToMoveStart() {
            if (player.StateMachine.Data.GroundData.MovementInput == Vector2.zero) {
                player.StateMachine.ChangeState(player.StateMachine.PlayerIdlingState);
                return;
            }
            
            if (player.StateMachine.Data.GroundData.ShouldWalk) {
                player.StateMachine.ChangeState(player.StateMachine.PlayerWalkStartState);
                return;
            }
            player.StateMachine.ChangeState(player.StateMachine.PlayerRunStartState); 
        }

        private void UnlockDashFront() {
            player.StateMachine.Data.AnimatorSettingData.DashFrontToLoopReady = true;
        }

        private void UnlockDashBack() {
            player.StateMachine.Data.AnimatorSettingData.DashBackAgainReady = true;
        }

        private void OnChangeDashToOtherState() {
            Debug.Log("触发动画事件: ChangeDashToOtherState");
            
            player.StateMachine.Data.AnimatorSettingData.DashFrontToLoopReady = false;
            player.StateMachine.Data.AnimatorSettingData.DashBackAgainReady = false;
            
            if (player.StateMachine.Data.GroundData.MovementInput == Vector2.zero) {
                OnChangeStateToIdle();
                return;
            }
            OnChangeStateToMoveLoop();
        }

        private void OnDashBackToIdleState() {
            if (player.StateMachine.Data.AnimatorSettingData.DashBackAgainReady) {
                OnChangeStateToIdle();
            }
        }

        private void UnlockSprintStop() {
            player.StateMachine.Data.AnimatorSettingData.SprintStopCache = true;
        }

        private void UnlockComboReady() {
            player.StateMachine.Data.AnimatorSettingData.ComboReady = true;
        }
        #endregion
    }
}