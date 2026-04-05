namespace Codebase
{
    public abstract class State
    {
        protected StatsController StatsController;
        protected StateSwitcher StateController;
        
        protected State(StateSwitcher stateController)
        {
            StateController = stateController;
            StatsController = stateController.GetStatsController();
        }
        
        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }
}