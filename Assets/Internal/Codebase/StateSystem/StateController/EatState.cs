namespace Codebase
{
    public class EatState : State
    {
        public EatState(StateController stateController) : base(stateController) { }

        public override void Enter()
        {
        }

        public override void Update()
        {
            StatsController.IncreaseCurrentStat(StatType.FoodStat);
            StatsController.IncreaseCurrentStat(StatType.HealthStat);
        }

        public override void Exit()
        {
        }
    }
}