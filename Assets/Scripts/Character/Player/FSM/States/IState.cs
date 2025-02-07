namespace qjklw.FSM.States
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update();
        public void FixedUpdate();
        public void HandleInput();
        
        public StateId Id { get; }
    }
}