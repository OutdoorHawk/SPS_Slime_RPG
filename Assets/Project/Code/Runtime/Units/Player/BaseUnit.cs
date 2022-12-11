using System;
using UnityEngine;

namespace Project.Code.Runtime.Units.Player
{
    public class BaseUnit : MonoBehaviour
    {
        protected virtual void CleanUp()
        {
            
        }

        private void OnDestroy()
        {
            CleanUp();
        }
    }
}