using System.Collections.Generic;
using Project.Code.Runtime.Roads;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.SceneContext
{
    public class SceneContextService : ISceneContextService
    {
         public Transform PlayerSpawnPoint { get; private set; }
         public RoadSpawner RoadSpawner { get; private set; }
         
         public void SetSpawnPoint(Transform playerSpawnPoint) => PlayerSpawnPoint = playerSpawnPoint;

         public void SetRoadSpawner(RoadSpawner roadSpawner) => RoadSpawner = roadSpawner;
    }
}