using UnityEngine;

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
            Debug.Log("Work state");
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            StateController.StopCoroutine(StateController.IncreaseBalance());
        }
    }
}