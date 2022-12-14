using Project.Code.Runtime.Roads;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.Runtime.World;
using Project.Code.UI.Windows.PlayerHUD;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SceneContext
{
    public interface ISceneContextService
    {
        Transform UnitsParent { get; }
        PlayerHUDWindow PlayerHUD { get; }
        PlayerSlime Player { get; }
        EnemySpawner EnemySpawner { get; }
        Transform PlayerSpawnPoint { get; }
        RoadSpawner RoadSpawner { get; }
        void SetSpawnPoint(Transform playerSpawnPoint);
        void SetRoadSpawner(RoadSpawner roadSpawner);
        void SetEnemySpawner(EnemySpawner enemySpawner);
        void SetPlayer(PlayerSlime player);
        public void SetPlayerHUD(PlayerHUDWindow playerHUD);
        void SetUnitsParent(Transform unitParent);
    }
}