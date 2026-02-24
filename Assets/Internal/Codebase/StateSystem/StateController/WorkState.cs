using UnityEngine;

namespace Codebase
{
    public class WorkState : State
    {
        private Coroutine workCoroutine;

        public WorkState(StateController stateController) : base(stateController) { }

        public override void Enter()
        {
            workCoroutine = StateController.StartCoroutine(StateController.IncreaseBalance());
            Debug.Log("Work state started");
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            if (workCoroutine != null)
            {
                StateController.StopCoroutine(workCoroutine);
                workCoroutine = null;
                Debug.Log("Work state stopped");
            }
        }
    }
}