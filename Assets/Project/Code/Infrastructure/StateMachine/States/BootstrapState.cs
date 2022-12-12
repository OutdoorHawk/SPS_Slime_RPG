using Project.Code.Infrastructure.Services.AssetProvider;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.StaticData.Units.Player;
using UnityEngine;

namespace Project.Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public BootstrapState(IStaticDataService staticDataService, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private IGameStateMachine _gameStateMachine;

        public void InitState(IGameStateMachine gameStateMachine) 
            => _gameStateMachine = gameStateMachine;

        public void Enter()
        {
            _staticDataService.Load();
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState>();
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
            _saveLoadService.SaveProgress();
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress();
            LoadDefaultValuesToProgress(progress);
            return progress;
        }

        private void LoadDefaultValuesToProgress(PlayerProgress playerProgress)
        {
            PlayerStaticData playerStaticData = _staticDataService.GetPlayerStaticData();
            playerProgress.PlayerStatsProgress.SetDefaultStatValues(playerStaticData.StatConfigs);
        }

        public void Exit()
        {
        }
    }
}