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
        }

        public override void Exit()
        {
        }
    }
}