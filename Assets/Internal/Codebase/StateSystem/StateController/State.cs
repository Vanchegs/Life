namespace Codebase
{
    public abstract class State
    {
        protected StatsController StatsController;
        protected StateController StateController;
        
        protected State(StateController stateController)
        {
            StateController = stateController;
            StatsController = stateController.GetStatsController();
        }
        
        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }
}