using Project.Code.Infrastructure.StateMachine.States;

namespace Project.Code.Infrastructure.StateMachine
{
    public interface IGameStateMachine

    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        IExitableState ActiveState { get; }
        void CleanUp();
    }
}