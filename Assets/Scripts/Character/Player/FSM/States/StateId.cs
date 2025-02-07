namespace qjklw.FSM.States
{
    public enum StateId
    {
        None,
        Base,
        
        Idle,
        
        WalkStart,
        WalkLoop,
        WalkStop,
        
        RunStart,
        RunLoop,
        RunStop,
        
        SprintLoop,
        SprintStop,
        
        SlideFront,
        SlideBack,
        
        Combo,
        RunAttack
    }
}