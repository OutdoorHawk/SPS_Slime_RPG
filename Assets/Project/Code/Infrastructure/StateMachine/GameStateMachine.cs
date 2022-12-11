using System;
using System.Collections.Generic;
using Project.Code.Infrastructure.StateMachine.States;
using Zenject;

namespace Project.Code.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        [Inject]
        public GameStateMachine(BootstrapState bootstrapState, LoadLevelState loadLevelState)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                { typeof(BootstrapState), bootstrapState },
                { typeof(LoadLevelState), loadLevelState },
            };
            foreach (var state in _states.Values) 
                state.InitState(this);
        }

        public IExitableState ActiveState { get; private set; }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            ActiveState?.Exit();

            var state = GetState<TState>();
            ActiveState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        public void CleanUp()
        {
            ActiveState = null;
            _states.Clear();
        }
    }
}