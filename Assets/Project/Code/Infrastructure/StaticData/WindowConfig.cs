using System;
using Project.Code.Infrastructure.Data;
using UnityEngine;

namespace Project.Code.Infrastructure.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        public WindowID ID;
        public MonoBehaviour WindowPrefab;
    }
}