using Project.Code.Infrastructure.Data;
using Project.Code.Runtime.Units.PlayerUnit;
using UnityEngine;

namespace Project.Code.StaticData.Units
{
    public abstract class UnitStaticData : ScriptableObject
    {
        [SerializeField] private BaseUnit _unitPrefab;
        
        public BaseUnit UnitPrefab => _unitPrefab;
    
    }
}