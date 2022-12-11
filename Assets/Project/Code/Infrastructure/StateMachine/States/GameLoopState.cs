namespace Project.Code.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private IGameStateMachine _gameStateMachine;

        public GameLoopState()
        {
            
        }

        public void Enter()
        {
           
        }

        public void InitState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
            
        }
    }
}