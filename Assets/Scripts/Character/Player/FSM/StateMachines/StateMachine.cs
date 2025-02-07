using System;
using qjklw.FSM.States;

namespace qjklw.FSM.StateMachines
{
    public class StateMachine 
    {
        public Player Player { get; }
        public StateId lastStateId { get; private set; } = StateId.None;
        protected IState currentState;

        public StateMachine(Player player) {
            Player = player;
        }

        public void ChangeState(IState newState) {
            lastStateId = currentState?.Id ?? StateId.None;
            
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
        
        public void Update() {
            currentState?.Update();
        }

        public void FixedUpdate() {
            currentState?.FixedUpdate();
        }

        public void HandleInput() {
            currentState?.HandleInput();
        }
    }
}