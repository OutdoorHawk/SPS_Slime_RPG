using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.Factory;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.SceneLoaderService;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Runtime.Roads;
using Project.Code.Runtime.Units.Player;
using Project.Code.Runtime.World;
using Project.Code.StaticData.Units;
using UnityEngine;

namespace Project.Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IState
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly ISceneContextService _sceneContextService;
        private readonly IEnemyFactory _enemyFactory;

        private LoadLevelState(ISceneLoader sceneLoader, IStaticDataService staticDataService,
            ISceneContextService sceneContextService, IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
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
            CreatePlayer();
            InitRoads();
            InitEnemySpawner();
        }

        private void CreatePlayer()
        {
            Vector3 playerSpawnPosition = _sceneContextService.PlayerSpawnPoint.position;
            Quaternion playerSpawnRotation = _sceneContextService.PlayerSpawnPoint.rotation;
            UnitStaticData staticData = _staticDataService.GetUnit(UnitID.Player);
            BaseUnit playerPrefab = staticData.UnitPrefab;
            PlayerSlime player = Object.Instantiate(playerPrefab, playerSpawnPosition,
                playerSpawnRotation).GetComponent<PlayerSlime>();
            player.Init(staticData);
            _sceneContextService.SetPlayer(player);
        }

        private void InitRoads()
        {
            RoadSpawner roadSpawner = _sceneContextService.RoadSpawner;
            roadSpawner.Init(_staticDataService.GetWorldStaticData(), _sceneContextService.Player.transform);
        }

        private void InitEnemySpawner()
        {
            EnemySpawner enemySpawner = _sceneContextService.EnemySpawner;
            enemySpawner.Init(_enemyFactory);
        }

        public void Exit()
        {
        }
    }
}