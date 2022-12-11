using System;
using System.Collections.Generic;
using Project.Code.Runtime.Units.EnemyUnit;

namespace Project.Code.Runtime.World
{
    public class UnitCollector
    {
        public static event Action OnEnemyAdded;
        public static event Action OnEnemyRemoved;
        public static List<Enemy> AliveEnemies { get; private set; }

        public void InitUnitLists()
        {
            AliveEnemies = new List<Enemy>();
        }

        public static void AddUnit(Enemy unit)
        {
            AliveEnemies.Add(unit);
            OnEnemyAdded?.Invoke();
        }

        public static void RemoveUnit(Enemy unit)
        {
            AliveEnemies.Remove(unit);
            OnEnemyRemoved?.Invoke();
        }

        public void Cleanup()
        {
            AliveEnemies?.Clear();
        }
    }
}