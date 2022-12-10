namespace SPS_Slime_RPG.Code.Infrastructure.StateMachine
{
    public interface IGameStateMachine

    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void CleanUp();
    }
}