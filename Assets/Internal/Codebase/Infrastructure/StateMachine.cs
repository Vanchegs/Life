using System;
using System.Collections.Generic;

namespace Codebase
{
    public class StateMachine
    {
        private State currentState;

        private Dictionary<Type, State> states = new();
        
        public void AddState(State newState) => 
            states[newState.GetType()] = newState;

        public void ChangeState<T>() where T : State
        {
            if (states.TryGetValue(typeof(T), out var newState))
            {
                currentState?.Exit();
                currentState = newState; 
                currentState.Enter();
            }
        }

        public void ExitState() => 
            currentState.Exit();

        public void UpdateCurrentState() => 
            currentState?.Update();

        public T GetState<T>() where T : State => 
            (T)states[typeof(T)];

        public Type GetCurrentState() => 
            currentState.GetType();
    }
}