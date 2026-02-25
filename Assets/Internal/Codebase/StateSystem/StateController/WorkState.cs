using UnityEngine;

namespace Codebase
{
    public class WorkState : State
    {
        public WorkState(StateController stateController) : base(stateController) { }

        public override void Enter()
        {
            StateController.StartWorkCoroutine();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            StateController.StopWorkCoroutine();
        }
    }
}