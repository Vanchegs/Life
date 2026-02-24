namespace Codebase
{
    public class WorkState : State
    {
        public WorkState(StateController stateController) : base(stateController)
        {
        }

        public override void Enter()
        {
            StateController.StartCoroutine(StateController.IncreaseBalance());
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            StateController.StopCoroutine(StateController.IncreaseBalance());
        }
    }
}