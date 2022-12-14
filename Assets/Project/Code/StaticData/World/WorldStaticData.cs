using System;
using Project.Code.Runtime.Roads;
using UnityEngine;

namespace Project.Code.StaticData.World
{
    [Serializable]
    public class WorldStaticData
    {
        [SerializeField] private float _roadMovingSpeed = 3f;
        [SerializeField] private float _playerWalkingTime = 3f;
        
        public float RoadMovingSpeed => _roadMovingSpeed;
        public float PlayerWalkingTime => _playerWalkingTime;
    }

   
}