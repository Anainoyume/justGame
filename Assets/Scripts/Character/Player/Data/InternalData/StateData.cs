using qjklw.Data.AnimationData;
using qjklw.FSM.StateMachines;

namespace qjklw.Data
{
    public class StateData
    {
        private MainStateMachine stateMachine;
        
        public PlayerGroundData GroundData { get; set; }
        public AnimatorSettingData AnimatorSettingData { get; set; }

        public StateData(MainStateMachine stateMachine) {
            this.stateMachine = stateMachine;
            
            GroundData = new PlayerGroundData();
            AnimatorSettingData = new AnimatorSettingData(stateMachine);
        }
    }
}