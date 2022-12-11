using System.Collections;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.CoroutineRunner;
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
        private IGameStateMachine _gameStateMachine;
        private readonly ISceneContextService _sceneContextService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly UnitCollector _unitCollector;

        private EnemySpawner _enemySpawner;
        private PlayerSlime _playerSlime;
        private RoadSpawner _roadSpawner;
        private WorldStaticData _worldStaticData;

        private enum GameLoopSubstate
        {
            Walking,
            Fighting
        }

        public GameLoopState(ISceneContextService sceneContextService, IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner)
        {
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
            Subscribe();
            DoFightState();
        }

        private void Subscribe()
        {
            _enemySpawner.OnWaveKilled += GoToWalkingState;
        }

        private void GoToWalkingState()
        {
            Debug.Log("walkingState");
            _coroutineRunner.StartCoroutine(WalkingRoutine());
        }

        private void Init()
        {
            _unitCollector.InitUnitLists();
            _playerSlime = _sceneContextService.Player;
            _worldStaticData = _staticDataService.GetWorldStaticData();
            _roadSpawner = _sceneContextService.RoadSpawner;
            _enemySpawner = _sceneContextService.EnemySpawner;
        }

        private IEnumerator WalkingRoutine()
        {
            float t = _worldStaticData.PlayerWalkingTime;
            do
            {
                _roadSpawner.TickMovement();
                t -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            } while (t > 0);

            DoFightState();
        }

        private void DoFightState()
        {
            _enemySpawner.SpawnWave();
        }

        private void Cleanup()
        {
            _enemySpawner.OnWaveKilled -= GoToWalkingState;
        }

        public void Exit()
        {
            Cleanup();
            _unitCollector.Cleanup();
        }
    }
}