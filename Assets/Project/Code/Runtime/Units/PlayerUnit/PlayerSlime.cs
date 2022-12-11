using System;
using Project.Code.Runtime.Units.Components.Damage;
using Project.Code.StaticData.Units;

namespace Project.Code.Runtime.Units.PlayerUnit
{
    public class PlayerSlime : BaseUnit
    {
        private PlayerStaticData _staticData;
        private PlayerDealDamageComponent _damageComponent;

        public override void Init(UnitStaticData unitStaticData)
        {
            base.Init(unitStaticData);
            _damageComponent = GetComponent<PlayerDealDamageComponent>();
            _damageComponent.Init(unitStaticData);
        }

        private void Update()
        {
            _damageComponent.UpdateTarget();
            _damageComponent.UpdateAttack();
        }
    }
}