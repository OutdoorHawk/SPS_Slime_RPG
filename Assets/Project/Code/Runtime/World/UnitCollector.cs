using System.Collections.Generic;
using Project.Code.Runtime.Units.EnemyUnit;

namespace Project.Code.Runtime.World
{
    public class UnitCollector
    {
        public static List<Enemy> AliveEnemies { get; private set; }

        public void InitUnitLists()
        {
            AliveEnemies = new List<Enemy>();
        }

        public static void AddUnit(Enemy unit)
        {
            AliveEnemies.Add(unit);
        }

        public static void RemoveUnit(Enemy unit)
        {
            AliveEnemies.Remove(unit);
        }

        public void Cleanup()
        {
            AliveEnemies?.Clear();
        }
    }
}