namespace Codebase
{
    public class EatState : State
    {
        public EatState(StateController stateController) : base(stateController) { }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            StatsController.IncreaseCurrentStat(StatType.FoodStat);
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}