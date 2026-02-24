using UnityEngine;

namespace Codebase
{
    public class IdleState : State
    {
        public IdleState(StateController stateController) : base(stateController) { }

        public override void Enter()
        {
            Debug.Log("Idle state is enter");
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
             Debug.Log("Current state is exit");
        }
    }
}