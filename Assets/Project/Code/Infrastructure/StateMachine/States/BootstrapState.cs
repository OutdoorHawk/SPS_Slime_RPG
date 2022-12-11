using Project.Code.Infrastructure.Services.AssetProvider;
using Project.Code.Infrastructure.Services.StaticData;

namespace Project.Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        private IGameStateMachine _gameStateMachine;

        public void InitState(IGameStateMachine gameStateMachine) 
            => _gameStateMachine = gameStateMachine;

        public void Enter()
        {
            _staticDataService.Load();
            _gameStateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
        }
    }
}