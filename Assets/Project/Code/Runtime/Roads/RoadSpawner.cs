using System.Collections.Generic;
using System.Linq;
using Project.Code.StaticData;
using UnityEngine;

namespace Project.Code.Runtime.Roads
{
    public class RoadSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _roadsParent;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _minTriggerDistance = 1f;

        [SerializeField] private List<Road> _activeRoads;
        private Road[] _roadPrefabs;
        private Transform _cachedTransform;
        private Transform _playerTransform;

        private static readonly Vector3 NextRoadOffset = new(50, 0, 0);
        private const float MAX_ROAD_AMOUNT = 4;

        public void Init(WorldStaticData worldStaticData, Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _movingSpeed = worldStaticData.RoadMovingSpeed;
            _roadPrefabs = worldStaticData.Roads;
            _cachedTransform = transform;
            _activeRoads = new List<Road>();
            CollectExistingRoads();
            SpawnFirstRoads();
        }

        private void CollectExistingRoads()
        {
            _activeRoads = _roadsParent.GetComponentsInChildren<Road>().ToList();
        }

        private void SpawnFirstRoads()
        {
            while (_activeRoads.Count < MAX_ROAD_AMOUNT)
                SpawnRoad(_activeRoads.Count * NextRoadOffset);
        }

        private void SpawnRoad(Vector3 nextRoadOffset)
        {
            int randomValue = Random.Range(0, _roadPrefabs.Length);
            Road road = Instantiate(_roadPrefabs[randomValue], nextRoadOffset,
                Quaternion.identity);
            road.transform.SetParent(_roadsParent, true);
            _activeRoads.Add(road);
        }

        private void Update()
        {
            if (_activeRoads.Count < 1)
                return;
            UpdateMovement();
            CheckNewRoadSpawn();
        }

        private void UpdateMovement()
        {
            for (int i = 0; i < _activeRoads.Count; i++)
            {
                Vector3 movementVector = _cachedTransform.forward * _movingSpeed;
                _activeRoads[i].Move(movementVector);
            }
        }

        private void CheckNewRoadSpawn()
        {
            if (_activeRoads[1].TriggerPoint == null)
                return;
            if (PlayerTouchedTrigger())
                SpawnNewRoad();
        }

        private bool PlayerTouchedTrigger()
        {
            return Vector3.Distance(_playerTransform.position, _activeRoads[1].TriggerPoint.position) <
                   _minTriggerDistance;
        }

        private void SpawnNewRoad()
        {
            _activeRoads[1].DestroyTrigger();
            Destroy(_activeRoads[0].gameObject);
            _activeRoads.RemoveAt(0);
            SpawnRoad(_activeRoads[^1].transform.position + NextRoadOffset);
        }
    }
}