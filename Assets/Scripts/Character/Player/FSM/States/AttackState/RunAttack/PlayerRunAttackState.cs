using qjklw.FSM.StateMachines;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.AttackState.RunAttack
{

    public class PlayerRunAttackState : BaseAttackState
    {
        public override StateId Id => StateId.RunAttack;
        public PlayerRunAttackState(MainStateMachine stateMachine) : base(stateMachine) { }



        #region Main Methods
        protected override void InitializeComboData() {
            base.InitializeComboData();
            ComboData = StateMachine.Player.RunAttackData;
        }
        #endregion



        #region Input Methods
        // RunAttack 禁止闪避
        protected override void OnDashStart(InputAction.CallbackContext obj) {

        }
        #endregion
    }

}