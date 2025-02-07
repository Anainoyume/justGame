using System.Linq;
using qjklw.Data;
using qjklw.FSM.StateMachines;
using UnityEngine.InputSystem;

namespace qjklw.FSM.States.AttackState
{
    /*
     *  攻击状态打算设计成一个 "微型状态机"，方便更加自由的配置
     * 
     */
    public class BaseAttackState : BaseGroundState
    {
        #region Variables
        protected PlayerComboData ComboData { get; set; }
        protected int ComboIndex { get; private set; } = 0;

        protected ComboData m_currentComboData;
        #endregion
        
        
        
        public BaseAttackState(MainStateMachine stateMachine) : base(stateMachine) {
            // 拿到玩家配置的连招数据
            // 直接在子类的构造函数中提供不一样的 ComboData 就可以了
            InitializeComboData();
        }
        
        
        #region IState Methods
        public override void Enter() {
            base.Enter();
            Data.AnimatorSettingData.ComboReady = false;
            // 使用动画的原生根运动
            Data.AnimatorSettingData.HandleRootMotion = false;
            
            // 判断连招数据, 如果连招表是空的
            if (!ComboData.m_comboList.Any()) {
                StateMachine.ChangeState(StateMachine.PlayerIdlingState);
                return;
            }
            
            // 首先会进入第一个连招, true 表示是第一个连招, 会进行一些初始化
            EnterToNextCombo(true);
        }
        

        public override void Exit() {
            base.Exit();
            Data.GroundData.EnableRotate = true;
            Data.AnimatorSettingData.HandleRootMotion = true;
        }

        #endregion



        #region Main Methods

        protected virtual void InitializeComboData() {
            
        }
        
        protected void EnterToNextCombo(bool isBegin = false) {
            ComboIndex = isBegin ? 0 : (ComboIndex + 1) % ComboData.m_comboList.Count;
            m_currentComboData = ComboData.m_comboList[ComboIndex];
            
            // 在这里处理一些初始化事件
            Data.GroundData.EnableRotate = m_currentComboData.m_allowRotate;
            Data.AnimatorSettingData.ComboReady = false;
            
            // 播放动画
            PlayAnimation(m_currentComboData.m_comboAnimationName);
        }

        #endregion



        #region Input Methods

        protected override void OnAttackStart(InputAction.CallbackContext ctx) {
            base.OnAttackStart(ctx);

            if (!Data.AnimatorSettingData.ComboReady) {
                return;
            }
            
            // 否则左键就允许切换到下一个了
            EnterToNextCombo();
        }

        #endregion
    }
}