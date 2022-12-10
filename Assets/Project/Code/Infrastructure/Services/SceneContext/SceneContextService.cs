using System.Collections.Generic;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SceneContext
{
    public class SceneContextService : ISceneContextService
    {
        private SceneContext _sceneContext;
        private List<Transform> _spawnPoints;

        public void CollectSceneContext()
        {
            _sceneContext = Object.FindObjectOfType<SceneContext>(true);
            CollectSceneSpawnPoints();
        }

        private void CollectSceneSpawnPoints()
        {
            _spawnPoints = new List<Transform>();
            for (int i = 0; i < _sceneContext.SpawnPointsParent.childCount; i++)
            {
                Transform spawn = _sceneContext.SpawnPointsParent.GetChild(i);
                _spawnPoints.Add(spawn);
            }
        }

        public List<Transform> GetSceneSpawnPoints()
            => _spawnPoints;
        
    }
}