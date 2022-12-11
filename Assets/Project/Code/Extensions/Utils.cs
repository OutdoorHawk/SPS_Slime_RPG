using System.Linq;
using Project.Code.Infrastructure.Data;
using Project.Code.Runtime.Units.EnemyUnit;
using Project.Code.Runtime.World;
using Project.Code.StaticData.Units.Player;
using UnityEngine;

namespace Project.Code.Extensions
{
    public static class Utils
    {
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        
        public static Enemy FindClosestTarget(Vector3 position)
        {
            float shortestDistance = Mathf.Infinity;
            Enemy nearestObject = null;
            foreach (var enemy in UnitCollector.AliveEnemies)
            {
                if (enemy == null)
                    continue;
                float distance = Vector3.Distance(position, enemy.transform.position);
                if (!(distance < shortestDistance)) 
                    continue;
                shortestDistance = distance;
                nearestObject = enemy;
            }

            return nearestObject;
        }

        public static StatData GetStatDataFromConfig(StatID id, StatConfig[] statConfigs)
        {
            return (from stat in statConfigs where stat.StatID == id select stat.StatData).FirstOrDefault();
        }
    }
}