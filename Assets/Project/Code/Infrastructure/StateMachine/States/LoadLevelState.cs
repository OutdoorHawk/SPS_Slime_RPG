using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.SceneLoaderService;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Runtime.Player;
using Project.Code.Runtime.Roads;
using UnityEngine;

namespace Project.Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IState
    {
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly ISceneContextService _sceneContextService;

        private LoadLevelState(ISceneLoader sceneLoader, IStaticDataService staticDataService,
            ISceneContextService sceneContextService)
        {
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
            InitRoads();
            CreatePlayer();
        }

        private void InitRoads()
        {
            RoadSpawner spawner = _sceneContextService.RoadSpawner;
            spawner.Init(_staticDataService.GetWorldStaticData());
        }

        private void CreatePlayer()
        {
            Vector3 playerSpawnPosition = _sceneContextService.PlayerSpawnPoint.position;
            Quaternion playerSpawnRotation = _sceneContextService.PlayerSpawnPoint.rotation;
            PlayerSlime player = Object.Instantiate(_staticDataService.GetPlayerPrefab(), playerSpawnPosition,
                playerSpawnRotation);
            player.Init(_staticDataService.GetPlayerStaticData());
        }

        public void Exit()
        {
        }
    }
}