namespace Codebase
{
    public class IdleState : State
    {
        public IdleState(StateSwitcher stateController) : base(stateController) { }

        public override void Enter()
        {
            StateController.GetCharacterView().HideCharacters();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
        }
    }
}