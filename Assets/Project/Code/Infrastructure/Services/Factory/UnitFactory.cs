using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Runtime.Units.Enemy;
using Project.Code.Runtime.Units.Player;
using Project.Code.StaticData.Units;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.Factory
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISceneContextService _sceneContextService;

        public UnitFactory(IStaticDataService staticDataService, ISceneContextService sceneContextService)
        {
            _sceneContextService = sceneContextService;
            _staticDataService = staticDataService;
        }

        public Enemy SpawnEnemy(Vector3 position, Quaternion rotation)
        {
            UnitStaticData staticData = _staticDataService.GetUnit(UnitID.Enemy);
            BaseUnit unitPrefab = staticData.UnitPrefab;
            Enemy enemy = Object.Instantiate(unitPrefab, position, rotation).GetComponent<Enemy>();
            PlayerSlime slime = _sceneContextService.Player;
            enemy.SetupPlayer(slime);
            enemy.Init(staticData);
            enemy.OnSpawn();
            return enemy;
        }

        public PlayerSlime SpawnPlayer(Vector3 position, Quaternion rotation)
        {
            UnitStaticData staticData = _staticDataService.GetUnit(UnitID.Player);
            BaseUnit unitPrefab = staticData.UnitPrefab;
            PlayerSlime player = Object.Instantiate(unitPrefab, position, rotation).GetComponent<PlayerSlime>();
            player.Init(staticData);
            _sceneContextService.SetPlayer(player);
            return player;
        }
    }
}