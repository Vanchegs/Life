namespace Codebase
{
    public class SleepState : State
    {
        public SleepState(StateSwitcher stateController) : base(stateController) { }

        public override void Enter()
        {
        }

        public override void Update()
        {
            StatsController.IncreaseCurrentStat(StatType.EnergyStat);
            StatsController.IncreaseCurrentStat(StatType.HealthStat);
        }

        public override void Exit()
        {
        }
    }
}