using Project.Code.Runtime.CustomData;
using Project.Code.Runtime.Units.Components;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.StaticData.Units;
using UnityEngine;

namespace Project.Code.Runtime.Units.Player
{
    [SelectionBase]
    [RequireComponent(typeof(DealDamageComponent))]
    [RequireComponent(typeof(HealthComponent))]
    public class BaseUnit : MonoBehaviour
    {
        public HealthComponent HealthComponent { get; private set; }
        protected UnitStaticData UnitStaticData { get; private set; }
        protected DealDamageComponent DealDamageComponent { get; private set; }

        public virtual void Init(UnitStaticData unitStaticData)
        {
            UnitStaticData = unitStaticData;
            DealDamageComponent = GetComponent<DealDamageComponent>();
            HealthComponent = GetComponent<HealthComponent>();
            HealthComponent.Init(UnitStaticData.HealthAmount);
            DealDamageComponent.Init(UnitStaticData);
            Subscribe();
        }

        protected virtual void Subscribe()
        {
            HealthComponent.OnReceivedDamage += HandleDamageTaken;
            HealthComponent.OnDeath += HandleDeath;
        }

        protected virtual void HandleDamageTaken(AttackDetails details)
        {
        }

        protected virtual void HandleDeath()
        {
           Destroy(gameObject);
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