using System.Collections;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.CoroutineRunner;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Runtime.Roads;
using Project.Code.Runtime.Units.Player;
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
        }

        public void InitState(IGameStateMachine gameStateMachine)
            => _gameStateMachine = gameStateMachine;

        public void Enter()
        {
            Init();
            DoFightState();
            //DoWalkingState();
        }

        private void Init()
        {
            _playerSlime = _sceneContextService.Player;
            _enemySpawner = _sceneContextService.EnemySpawner;
            _enemySpawner.Init(_playerSlime, _staticDataService.GetUnit(UnitID.Enemy));
            _worldStaticData = _staticDataService.GetWorldStaticData();
            _roadSpawner = _sceneContextService.RoadSpawner;
        }

        private void DoWalkingState()
        {
            _coroutineRunner.StartCoroutine(WalkingRoutine());
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

        public void Exit()
        {
        }
    }
}