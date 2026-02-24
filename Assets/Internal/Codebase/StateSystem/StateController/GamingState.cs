namespace Codebase
{
    public class GamingState : State
    {
        public GamingState(StateController stateController) : base(stateController) { }
        
        public override void Enter()
        {
        }

        public override void Update()
        {
            StatsController.IncreaseCurrentStat(StatType.MentalHealthStat);
        }

        public override void Exit()
        {
        }
    }
}