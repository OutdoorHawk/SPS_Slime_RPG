using Project.Code.Runtime.Units.Enemy;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.Factory
{
    public interface IEnemyFactory
    {
        Enemy SpawnEnemy(Vector3 position, Quaternion rotation);
    }
}