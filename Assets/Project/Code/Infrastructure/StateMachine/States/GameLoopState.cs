using System.Collections;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.CoroutineRunner;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Runtime.Roads;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.Runtime.World;
using Project.Code.StaticData;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly ISceneContextService _sceneContextService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly UnitCollector _unitCollector;

        private IGameStateMachine _gameStateMachine;
        private EnemySpawner _enemySpawner;
        private PlayerSlime _playerSlime;
        private RoadSpawner _roadSpawner;
        private WorldStaticData _worldStaticData;
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _progressService;
        
        public GameLoopState(ISceneContextService sceneContextService, IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _sceneContextService = sceneContextService;
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
            _unitCollector = new UnitCollector();
        }

        public void InitState(IGameStateMachine gameStateMachine)
            => _gameStateMachine = gameStateMachine;

        public void Enter()
        {
            Init();
            StartGame();
        }

        private void Init()
        {
            _unitCollector.InitUnitLists();
            _playerSlime = _sceneContextService.Player;
            _worldStaticData = _staticDataService.GetWorldStaticData();
            _roadSpawner = _sceneContextService.RoadSpawner;
            _enemySpawner = _sceneContextService.EnemySpawner;
        }

        private void StartGame()
        {
            _roadSpawner.DoWalking(OnWalkingDone);
        }

        private void OnFightCompleted()
        {
            _progressService.Progress.PlayerLevelsProgress.PassFight();
            _roadSpawner.DoWalking(OnWalkingDone);
        }

        private void OnWalkingDone()
        {
            _enemySpawner.SpawnWave(OnFightCompleted);
        }

        public void Exit()
        {
            _unitCollector.Cleanup();
        }
    }
}