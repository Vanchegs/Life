using UnityEngine;

namespace Codebase
{
    public class IdleState : State
    {
        public override void Enter()
        {
            Debug.Log("Current state is enter");
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
             Debug.Log("Current state is exit");
        }
    }
}