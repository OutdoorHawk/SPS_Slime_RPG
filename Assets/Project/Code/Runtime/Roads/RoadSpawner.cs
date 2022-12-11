using System;
using System.Collections.Generic;
using Project.Code.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Runtime.Roads
{
    public class RoadSpawner : MonoBehaviour
    {
        [SerializeField]private WorldStaticData _worldStaticData;

        private Road[] _roadPrefabs;
        private List<Road> _activeRoads = new();
        private Transform _cachedTransform;

        private const float NEXT_ROAD_OFFSET = 50;
        private const float MAX_ROAD_AMOUNT = 4;

        public void Init(WorldStaticData worldStaticData)
        {
            _worldStaticData = worldStaticData;
            _roadPrefabs = _worldStaticData.Roads;
            _cachedTransform = transform;
            SpawnFirstRoads();
        }

        private void CleanChildren()
        {
            for (int i = 0; i < transform.childCount; i++) 
                Destroy(transform.GetChild(i).gameObject);
        }

        private void SpawnFirstRoads()
        {
            for (int i = 0; i < MAX_ROAD_AMOUNT; i++) 
                SpawnRoad(i * NEXT_ROAD_OFFSET);
        }

        private void SpawnRoad(float nextRoadOffset)
        {
            Vector3 roadOffset = new Vector3(nextRoadOffset, 0, 0);
            int randomValue = Random.Range(0, _roadPrefabs.Length);
            Road road = Instantiate(_roadPrefabs[randomValue],  roadOffset,
               Quaternion.identity);
            _activeRoads.Add(road);
        }

        private void Update()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            for (int i = 0; i < _activeRoads.Count; i++)
            {
                Vector3 movementVector = _cachedTransform.forward * _worldStaticData.RoadMovingSpeed;
                _activeRoads[i].Move(movementVector);
            }
        }
    }
}