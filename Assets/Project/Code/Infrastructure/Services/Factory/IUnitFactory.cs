using Project.Code.Runtime.Units.EnemyUnit;
using Project.Code.Runtime.Units.PlayerUnit;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.Factory
{
    public interface IUnitFactory
    {
        Enemy SpawnEnemy(Vector3 position, Quaternion rotation);
        PlayerSlime SpawnPlayer(Vector3 position, Quaternion rotation);
    }
}