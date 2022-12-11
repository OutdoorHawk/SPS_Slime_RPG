using System;
using Project.Code.Runtime.CustomData;
using Project.Code.Runtime.Units.Components;
using Project.Code.StaticData.Units;
using UnityEngine;

namespace Project.Code.Runtime.Units.Player
{
    [SelectionBase]
    public class BaseUnit : MonoBehaviour
    {
        public HealthComponent HealthComponent { get; private set; }
        protected UnitStaticData UnitStaticData { get; private set; }
        
        public virtual void Init(UnitStaticData unitStaticData)
        {
            UnitStaticData = unitStaticData;
            HealthComponent = GetComponent<HealthComponent>();
            HealthComponent.Init(UnitStaticData.HealthAmount);
            Subscribe();
        }

        protected virtual void Subscribe()
        {
            HealthComponent.OnReceivedDamage += HandleDamageTaken;
            HealthComponent.OnDeath += HandleDeath;
        }

        private void HandleDamageTaken(AttackDetails details)
        {
            
        }

        private void HandleDeath()
        {
            
        }

        protected virtual void CleanUp()
        {
            HealthComponent.OnReceivedDamage -= HandleDamageTaken;
            HealthComponent.OnDeath -= HandleDeath;
        }

        private void OnDestroy()
        {
            CleanUp();
        }
    }
}