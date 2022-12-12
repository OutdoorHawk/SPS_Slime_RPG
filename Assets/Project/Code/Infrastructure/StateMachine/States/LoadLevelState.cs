using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.Factory;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.SceneLoaderService;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.Services.UI;
using Project.Code.Runtime.Roads;
using Project.Code.Runtime.World;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IState
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly ISceneContextService _sceneContextService;
        private readonly IUnitFactory _unitFactory;
        private readonly IUIFactory _uiFactory;
        private RectTransform _hpPanel;
        private IPersistentProgressService _progressService;

        private LoadLevelState(ISceneLoader sceneLoader, IStaticDataService staticDataService,
            ISceneContextService sceneContextService, IUnitFactory unitFactory, IUIFactory uiFactory,
            IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _unitFactory = unitFactory;
            _uiFactory = uiFactory;
            _sceneContextService = sceneContextService;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public void InitState(IGameStateMachine gameStateMachine)
            => _gameStateMachine = gameStateMachine;

        public void Enter()
        {
            _sceneLoader.LoadScene(Constants.GAME_SCENE, OnLoaded);
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.Enter<GameLoopState>();
        }
        
        private void InitGameWorld()
        {
            InitUI();
            CreatePlayer();
            InitRoads();
            InitEnemySpawner();
        }

        private void InitUI()
        {
            _uiFactory.CreateUiRoot();
            _uiFactory.CreatePlayerHUD();
            _hpPanel = _uiFactory.CreateWindow(WindowID.HealthBars).GetComponent<RectTransform>();
        }

        private void CreatePlayer()
        {
            Vector3 playerSpawnPosition = _sceneContextService.PlayerSpawnPoint.position;
            Quaternion playerSpawnRotation = _sceneContextService.PlayerSpawnPoint.rotation;
            _unitFactory.SpawnPlayer(playerSpawnPosition, playerSpawnRotation, _hpPanel);
        }

        private void InitRoads()
        {
            RoadSpawner roadSpawner = _sceneContextService.RoadSpawner;
            roadSpawner.Init(_staticDataService.GetWorldStaticData(), _sceneContextService.Player.transform);
        }

        private void InitEnemySpawner()
        {
            int currentLevel = _progressService.Progress.PlayerLevelsProgress.CurrentLevel;
            EnemySpawner enemySpawner = _sceneContextService.EnemySpawner;
            enemySpawner.Init(_unitFactory, _hpPanel, _staticDataService.GetLevelStaticData(currentLevel));
        }

        public void Exit()
        {
        }
    }
}