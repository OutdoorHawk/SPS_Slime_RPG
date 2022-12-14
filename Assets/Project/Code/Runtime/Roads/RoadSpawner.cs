using System;
using System.Collections;
using System.Collections.Generic;
using Project.Code.StaticData.World;
using Project.Code.UI.BG;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Runtime.Roads
{
    public class RoadSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _roadsParent;
        [SerializeField] private ParticleSystem _windParticles;
        [SerializeField] private Parallax _parallax;
        [SerializeField] private float _minTriggerDistance = 10f;

        private List<Road> _activeRoads;
        private Road[] _roadPrefabs;
        private Transform _cachedTransform;
        private Transform _playerTransform;

        private static readonly Vector3 NextRoadOffset = new(50, 0, 0);
        private float _movingSpeed;
        private float _walkingTime;

        private const float MAX_ROAD_AMOUNT = 4;

        public void Init(WorldStaticData worldStaticData, Transform playerTransform,
            LevelStaticData levelStaticData)
        {
            _playerTransform = playerTransform;
            _movingSpeed = worldStaticData.RoadMovingSpeed;
            _roadPrefabs = levelStaticData.Roads;
            _walkingTime = worldStaticData.PlayerWalkingTime;
            _cachedTransform = transform;
            _activeRoads = new List<Road>();
            _windParticles.Stop();
            DestroyExistingRoads();
            SpawnFirstRoads();
        }

        private void DestroyExistingRoads()
        {
            for (int i = 0; i < _roadsParent.childCount; i++)
                Destroy(_roadsParent.GetChild(i).gameObject);
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

        public void DoWalking(Action OnWalkingDone)
        {
            StartCoroutine(WalkingRoutine(OnWalkingDone));
        }

        private IEnumerator WalkingRoutine(Action onWalkingDone)
        {
            float t = _walkingTime;
            _windParticles.Play();
            do
            {
                TickMovement();
                t -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            } while (t > 0);

            onWalkingDone?.Invoke();
            _windParticles.Stop();
        }

        private void TickMovement()
        {
            UpdateMovement();
            CheckNewRoadSpawn();
            _parallax.UpdateParallax();
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
            if (PlayerTouchedTrigger())
                SpawnNewRoad();
        }

        private bool PlayerTouchedTrigger()
        {
            if (_playerTransform == null)
                return false;
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