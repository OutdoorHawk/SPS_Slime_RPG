using System.Collections.Generic;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SceneContext
{
    public interface ISceneContextService 
    {
        void CollectSceneContext();
        List<Transform> GetSceneSpawnPoints();
    }
}