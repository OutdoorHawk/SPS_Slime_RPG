using System;
using DG.Tweening;
using UnityEngine;

namespace Project.Code.Runtime.Roads
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private Transform _triggerPoint;
        public Transform TriggerPoint => _triggerPoint;

        public void Move(Vector3 movementVector)
        {
            transform.Translate(movementVector * Time.deltaTime);
        }

        public void DestroyTrigger() => Destroy(_triggerPoint.gameObject);
    }
}