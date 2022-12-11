using Project.Code.StaticData;
using Project.Code.StaticData.Units;

namespace Project.Code.Runtime.Units.Player
{
    public class PlayerSlime : BaseUnit
    {
        private PlayerStaticData _staticData;

        public void Init(UnitStaticData unitStaticData)
        {
            _staticData = unitStaticData as PlayerStaticData;
        }
        
    }
}