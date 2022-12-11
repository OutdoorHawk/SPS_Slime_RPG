using Project.Code.Runtime.Roads;
using Project.Code.Runtime.Units.Player;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.Runtime.World;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SceneContext
{
    public class SceneContextService : ISceneContextService
    {
        public PlayerSlime Player { get; private set; }
        public EnemySpawner EnemySpawner { get; private set; }
        public Transform PlayerSpawnPoint { get; private set; }
        public RoadSpawner RoadSpawner { get; private set; }

        public void SetSpawnPoint(Transform playerSpawnPoint) => PlayerSpawnPoint = playerSpawnPoint;

        public void SetRoadSpawner(RoadSpawner roadSpawner) => RoadSpawner = roadSpawner;

        public void SetEnemySpawner(EnemySpawner enemySpawner) => EnemySpawner = enemySpawner;
        public void SetPlayer(PlayerSlime player) => Player = player;
    }
}