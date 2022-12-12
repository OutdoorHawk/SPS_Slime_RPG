using System;

namespace Project.Code.Runtime.CustomData
{
    [Serializable]
    public struct AttackDetails
    {
        public float Damage;
        public float Crit;
        
        public static readonly int CritValue = 3;

        public AttackDetails(float damage)
        {
            Damage = damage;
            Crit = 0;
        }
    }
}
