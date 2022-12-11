using System.Collections.Generic;
using Project.Code.Runtime.World;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SceneContext
{
    public interface ISceneContextService 
    {
         Transform PlayerSpawnPoint { get;  }
         RoadSpawner RoadSpawner { get; }
         void SetSpawnPoint(Transform playerSpawnPoint);
         void SetRoadSpawner(RoadSpawner roadSpawner);
    }
}