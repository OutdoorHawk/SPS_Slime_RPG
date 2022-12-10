using UnityEngine;

namespace SPS_Slime_RPG.Code.Infrastructure.Services.SceneContext
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointsParent;

        public Transform SpawnPointsParent => _spawnPointsParent;
        
        
    }
}
