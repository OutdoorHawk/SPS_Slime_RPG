using System;
using SPS_Slime_RPG.Code.Infrastructure.Data;
using UnityEngine;

namespace SPS_Slime_RPG.Code.Infrastructure.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        public WindowID ID;
        public MonoBehaviour WindowPrefab;
    }
}