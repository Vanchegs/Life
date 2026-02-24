namespace Codebase
{
    public class GamingState : State
    {
        public GamingState(StateController stateController) : base(stateController) { }
        
        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            StatsController.IncreaseCurrentStat(StatType.MentalHealthStat);
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}