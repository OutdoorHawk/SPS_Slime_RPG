using System;
using Project.Code.Infrastructure.Services.CoroutineRunner;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Runtime.Roads;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.Runtime.World;
using Project.Code.StaticData.World;
using Project.Code.UI.Windows.PlayerHUD;
using Object = UnityEngine.Object;

namespace Project.Code.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly ISceneContextService _sceneContextService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly UnitCollector _unitCollector;
        private readonly IPersistentProgressService _progressService;

        private IGameStateMachine _gameStateMachine;
        private EnemySpawner _enemySpawner;
        private PlayerSlime _playerSlime;
        private RoadSpawner _roadSpawner;
        private WorldStaticData _worldStaticData;
        private ISaveLoadService _saveLoadService;
        private PlayerLevelsProgress _levelsProgress;
        private LevelStaticData _levelStaticData;
        private PlayerHUDWindow _playerHUD;

        public GameLoopState(ISceneContextService sceneContextService, IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
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
            _levelsProgress = _progressService.Progress.PlayerLevelsProgress;
            _levelStaticData = _staticDataService.GetLevelStaticData(_levelsProgress.CurrentLevel);
            _playerHUD = _sceneContextService.PlayerHUD;
        }

        private void StartGame()
        {
            _roadSpawner.DoWalking(EnableFight);
            _playerSlime.SetWalkingState();
            _playerSlime.OnUnitDead += RestartLevel;
        }

        private void RestartLevel(BaseUnit player)
        {
            _playerHUD.EnableDefeatTitle(() =>  _gameStateMachine.Enter<LoadLevelState>());
            _levelsProgress.ResetFights();
            //  _saveLoadService.SaveProgress(); // TODO ACTIVATE LATER
        }

        private void EnableFight()
        {
            _enemySpawner.SpawnWave(OnFightCompleted);
            _playerSlime.SetFightState();
        }

        private void OnFightCompleted()
        {
            _levelsProgress.PassFight();

            if (AllFightsPassed())
                ContinueWalking(EnterBossFight);
            else
                ContinueWalking(EnableFight);

            //  _saveLoadService.SaveProgress(); // TODO ACTIVATE LATER
        }

        private void ContinueWalking(Action onDone)
        {
            _roadSpawner.DoWalking(onDone);
            _playerSlime.SetWalkingState();
        }

        private bool AllFightsPassed()
        {
            return _levelsProgress.CurrentFight == _levelStaticData.MaxFightsOnLevel;
        }

        private void EnterBossFight()
        {
            _playerHUD.EnableBossTitle();
            _enemySpawner.SpawnBoss(CompleteLevel);
            _playerSlime.SetFightState();
        }

        private void CompleteLevel()
        {
            _levelsProgress.PassLevel();
            _gameStateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
            _playerSlime.OnUnitDead -= RestartLevel;
            _unitCollector.Cleanup();
        }
    }
}