using System;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.UpdateBehavior
{
    public class UpdateBehaviour : MonoBehaviour, IUpdateBehaviourService
    {
        public event Action UpdateEvent;
        public event Action FixedUpdateEvent;

        private void Update()
        {
            UpdateEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }
    }
}