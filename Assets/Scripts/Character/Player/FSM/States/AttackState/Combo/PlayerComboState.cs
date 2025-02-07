using System;
using System.Linq;
using qjklw.Data;
using qjklw.FSM.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.AttackState.Combo
{
    /*
     *  我打算把连击类做成一个 "小型状态机"，可以高自由度配置连招 
     */
    public class PlayerComboState : BaseAttackState
    {
        public override StateId Id => StateId.Combo;
        public PlayerComboState(MainStateMachine stateMachine) : base(stateMachine) { }



        #region Main Methods
        protected override void InitializeComboData() {
            base.InitializeComboData();

            ComboData = StateMachine.Player.ComboData;
        }
        #endregion
    }
}