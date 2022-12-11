using System;

namespace Project.Code.Runtime.CustomData
{
    [Serializable]
    public struct AttackDetails
    {
        public float Damage;

        public AttackDetails(float damage)
        {
            Damage = damage;
        }
    }
}
