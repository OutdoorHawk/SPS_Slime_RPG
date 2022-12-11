using System;
using UnityEngine;

namespace Project.Code.StaticData.World
{
    [CreateAssetMenu(fileName = "EnemySpawnerStaticData", menuName = "Static Data/EnemySpawnerStaticData")]
    public class EnemySpawnerStaticData : ScriptableObject
    {
        [SerializeField] private int _enemiesMinAmount = 1;
        [SerializeField] private int _enemiesMaxAmount = 4;

        public int EnemiesMinAmount => _enemiesMinAmount;
        public int EnemiesMaxAmount => _enemiesMaxAmount;
    }
}