using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SceneContext;
using Project.Code.Infrastructure.Services.StaticData;
using Project.Code.Runtime.Units.Enemy;
using Project.Code.Runtime.Units.Player;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISceneContextService _sceneContextService;

        public EnemyFactory(IStaticDataService staticDataService, ISceneContextService sceneContextService)
        {
            _sceneContextService = sceneContextService;
            _staticDataService = staticDataService;
        }

        public Enemy SpawnEnemy(Vector3 position, Quaternion rotation)
        {
            BaseUnit unitPrefab = _staticDataService.GetUnit(UnitID.Enemy).UnitPrefab;
            Enemy enemy = Object.Instantiate(unitPrefab, position, rotation).GetComponent<Enemy>();
            PlayerSlime slime = _sceneContextService.Player;
            enemy.Init(slime);
            enemy.OnSpawn();
            return enemy;
        }
    }
}