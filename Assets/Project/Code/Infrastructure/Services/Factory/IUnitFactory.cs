using Project.Code.Runtime.Units.Enemy;
using Project.Code.Runtime.Units.Player;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.Factory
{
    public interface IUnitFactory
    {
        Enemy SpawnEnemy(Vector3 position, Quaternion rotation);
        PlayerSlime SpawnPlayer(Vector3 position, Quaternion rotation);
    }
}