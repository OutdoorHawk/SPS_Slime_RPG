using System;
using Project.Code.Runtime.Roads;
using UnityEngine;

namespace Project.Code.StaticData.World
{
    [Serializable]
    public class WorldStaticData
    {
        [SerializeField] private Road[] _roads;
        [SerializeField] private float _roadMovingSpeed = 3f;
        [SerializeField] private float _playerWalkingTime = 3f;

        public Road[] Roads => _roads;
        public float RoadMovingSpeed => _roadMovingSpeed;
        public float PlayerWalkingTime => _playerWalkingTime;
    }

   
}