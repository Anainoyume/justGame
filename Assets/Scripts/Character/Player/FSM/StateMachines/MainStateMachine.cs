using qjklw.Data;
using qjklw.FSM.States;
using qjklw.FSM.States.AttackState.Combo;
using qjklw.FSM.States.AttackState.RunAttack;
using qjklw.FSM.States.AttackState.SpecialAttack;
using qjklw.FSM.States.DashState.SlideState;
using qjklw.FSM.States.MovementState.End;
using qjklw.FSM.States.MovementState.Loop;
using qjklw.FSM.States.MovementState.Start;

namespace qjklw.FSM.StateMachines
{
    public class MainStateMachine : StateMachine
    {
        public StateData Data { get; set; }
        // 缓存一些状态
        public PlayerIdlingState PlayerIdlingState { get; private set; }
        
        public PlayerRunStartState PlayerRunStartState { get; private set; }
        public PlayerRunLoopState PlayerRunLoopState { get; private set; }
        public PlayerRunStopState PlayerRunStopState { get; private set; }
        
        public PlayerWalkStartState PlayerWalkStartState { get; private set; }
        public PlayerWalkLoopState PlayerWalkLoopState { get; private set; }
        public PlayerWalkStopState PlayerWalkStopState { get; private set; }
        
        public PlayerSprintLoopState PlayerSprintLoopState { get; private set; }
        public PlayerSprintStopState PlayerSprintStopState { get; private set; }
        
        public PlayerSlideFrontState PlayerSlideFrontState { get; private set; }
        public PlayerSlideBackState PlayerSlideBackState { get; private set; }
        
        public PlayerComboState PlayerComboState { get; private set; }
        public PlayerSpecialAttackState PlayerSpecialAttackState { get; private set; }
        public PlayerRunAttackState PlayerRunAttackState { get; private set; }
        
        public MainStateMachine(Player player) : base(player) {
            Data = new StateData(this);
            PlayerIdlingState = new PlayerIdlingState(this);

            PlayerRunStartState = new PlayerRunStartState(this);
            PlayerRunLoopState = new PlayerRunLoopState(this);
            PlayerRunStopState = new PlayerRunStopState(this);
            
            PlayerWalkStartState = new PlayerWalkStartState(this);
            PlayerWalkLoopState = new PlayerWalkLoopState(this);
            PlayerWalkStopState = new PlayerWalkStopState(this);
            
            PlayerSprintLoopState = new PlayerSprintLoopState(this);
            PlayerSprintStopState = new PlayerSprintStopState(this);

            PlayerSlideFrontState = new PlayerSlideFrontState(this);
            PlayerSlideBackState = new PlayerSlideBackState(this);

            PlayerComboState = new PlayerComboState(this);
            PlayerRunAttackState = new PlayerRunAttackState(this);
            PlayerSpecialAttackState = new PlayerSpecialAttackState(this);
        }
    }
}