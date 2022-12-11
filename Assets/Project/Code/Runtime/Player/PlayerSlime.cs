using System;
using Project.Code.StaticData;
using UnityEngine;

namespace Project.Code.Runtime.Player
{
    public class PlayerSlime : MonoBehaviour
    {
        private PlayerStaticData _staticData;

        public void Init(PlayerStaticData playerStaticData)
        {
            _staticData = playerStaticData;
        }
        
    }
}