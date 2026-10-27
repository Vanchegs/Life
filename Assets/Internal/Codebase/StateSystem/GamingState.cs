namespace Codebase
{
    public class GamingState : State
    {
        public GamingState(StateSwitcher stateController) : base(stateController) { }
        
        public override void Enter()
        {
            StateController.GetCharacterView().ShowGaming();
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