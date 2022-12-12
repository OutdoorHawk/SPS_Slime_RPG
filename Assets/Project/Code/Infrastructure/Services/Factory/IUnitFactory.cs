using Project.Code.Runtime.Units.EnemyUnit;
using Project.Code.Runtime.Units.PlayerUnit;
using Project.Code.StaticData.World;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.Factory
{
    public interface IUnitFactory
    {
        Enemy SpawnEnemy(Vector3 position, Quaternion rotation, RectTransform rectTransform);
        PlayerSlime SpawnPlayer(Vector3 position, Quaternion rotation, RectTransform rectTransform);
    }
}