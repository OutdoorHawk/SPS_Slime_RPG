using Project.Code.Runtime.Roads;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Services.SceneContext
{
    public class SceneContextComponent : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private RoadSpawner _roadSpawner;
        
        private ISceneContextService _sceneContextService;

        [Inject]
        private void Construct(ISceneContextService sceneContextService)
        {
            _sceneContextService = sceneContextService;
            _sceneContextService.SetSpawnPoint(_playerSpawnPoint);
            _sceneContextService.SetRoadSpawner(_roadSpawner);
        }
    }
}
