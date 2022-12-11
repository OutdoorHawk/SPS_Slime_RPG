using System;
using Project.Code.Runtime.Roads;
using UnityEngine;

namespace Project.Code.StaticData
{
    [Serializable]
    public class WorldStaticData
    {
        [SerializeField] private Road[] _roads;
        [SerializeField] private float _roadMovingSpeed = 3f;

        public float RoadMovingSpeed => _roadMovingSpeed;

        public Road[] Roads => _roads;
    }
}