using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SaveLoadService;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Infrastructure.Services.ZenjectFactory;
using Project.Code.Runtime.Units.EnemyUnit;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.StaticData.Units;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.Factory
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISceneContextService _sceneContextService;
        private readonly IPersistentProgressService _progressService;
        private IZenjectFactory _zenjectFactory;

        public UnitFactory(IStaticDataService staticDataService, ISceneContextService sceneContextService,
            IPersistentProgressService progressService, IZenjectFactory zenjectFactory)
        {
            _zenjectFactory = zenjectFactory;
            _sceneContextService = sceneContextService;
            _progressService = progressService;
            _staticDataService = staticDataService;
        }

        public Enemy SpawnEnemy(Vector3 position, Quaternion rotation, RectTransform hpPanel)
        {
            int currentLevel = _progressService.Progress.PlayerLevelsProgress.CurrentLevel;
            LevelStaticData levelStaticData = _staticDataService.GetLevelStaticData(currentLevel);
            EnemyStaticData staticData = levelStaticData.GetEnemyStaticData();
            BaseUnit unitPrefab = staticData.UnitPrefab;
            Enemy enemy = _zenjectFactory.Instantiate(unitPrefab, position, rotation).GetComponent<Enemy>();
            PlayerSlime slime = _sceneContextService.Player;
            enemy.SetupPlayer(slime);
            enemy.Init(staticData, _progressService.Progress, hpPanel);
            enemy.OnSpawn();
            return enemy;
        }

        public PlayerSlime SpawnPlayer(Vector3 position, Quaternion rotation, RectTransform hpPanel)
        {
            UnitStaticData staticData = _staticDataService.GetUnit(UnitID.Player);
            BaseUnit unitPrefab = staticData.UnitPrefab;
            PlayerSlime player = _zenjectFactory.Instantiate(unitPrefab, position, rotation).GetComponent<PlayerSlime>();
            player.Init(staticData, _progressService.Progress, hpPanel);
            _sceneContextService.SetPlayer(player);
            return player;
        }

        public Enemy SpawnBoss(Vector3 position, Quaternion rotation, RectTransform hpPanel)
        {
            int currentLevel = _progressService.Progress.PlayerLevelsProgress.CurrentLevel;
            LevelStaticData levelStaticData = _staticDataService.GetLevelStaticData(currentLevel);
            EnemyStaticData staticData = levelStaticData.GetBossStaticData();
            BaseUnit unitPrefab = staticData.UnitPrefab;
            Enemy enemy = _zenjectFactory.Instantiate(unitPrefab, position, rotation).GetComponent<Enemy>();
            PlayerSlime slime = _sceneContextService.Player;
            enemy.SetupPlayer(slime);
            enemy.Init(staticData, _progressService.Progress, hpPanel);
            enemy.OnSpawn();
            return enemy;
        }
    }
}