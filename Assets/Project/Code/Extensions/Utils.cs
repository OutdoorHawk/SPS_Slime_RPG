using System.Linq;
using Project.Code.Infrastructure.Data;
using Project.Code.Infrastructure.Services.SaveLoadService.Progress;
using Project.Code.Infrastructure.Services.StaticData;
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
        
        public static void LoadDefaultValuesToProgress(PlayerProgress playerProgress, IStaticDataService staticDataService)
        {
            PlayerStaticData playerStaticData = staticDataService.GetPlayerStaticData();
            playerProgress.PlayerStatsProgress.SetDefaultStatValues(playerStaticData.StatConfigs);
        }

        public static Enemy[] FindTargets()
        {
            Enemy[] enemies = UnitCollector.AliveEnemies.ToArray();
            return enemies;
        }
        
        public static bool VibrationEnabled() => 
            PlayerPrefs.GetInt(Constants.VIBRATION) == 0;

        public static StatData GetStatDataFromConfig(StatID id, StatConfig[] statConfigs) => 
            (from stat in statConfigs where stat.StatID == id select stat.StatData).FirstOrDefault();
    }
}