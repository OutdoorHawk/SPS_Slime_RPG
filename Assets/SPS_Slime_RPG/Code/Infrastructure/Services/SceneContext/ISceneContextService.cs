using System.Collections.Generic;
using UnityEngine;

namespace SPS_Slime_RPG.Code.Infrastructure.Services.SceneContext
{
    public interface ISceneContextService 
    {
        void CollectSceneContext();
        List<Transform> GetSceneSpawnPoints();
    }
}