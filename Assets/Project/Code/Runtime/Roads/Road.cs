using UnityEngine;

namespace Project.Code.Runtime.Roads
{
    public class Road : MonoBehaviour
    {
        public void Move(Vector3 movementVector)
        {
            transform.Translate(movementVector * Time.deltaTime);
        }
    }
}