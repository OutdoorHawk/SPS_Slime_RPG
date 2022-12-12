using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure.Services.ZenjectFactory
{
    public class ZenjectFactory : IZenjectFactory
    {
        private readonly DiContainer _container;

        public ZenjectFactory(DiContainer container) => _container = container;

        public GameObject Instantiate(GameObject gameObject) =>
            _container.InstantiatePrefab(gameObject);

        public GameObject Instantiate(GameObject gameObject, Transform parent) =>
            _container.InstantiatePrefab(gameObject, parent);

        public T Instantiate<T>(T behaviour) where T : MonoBehaviour =>
            _container.InstantiatePrefab(behaviour).GetComponent<T>();

        public T Instantiate<T>(T behaviour, Transform parent) where T : MonoBehaviour =>
            _container.InstantiatePrefab(behaviour, parent).GetComponent<T>();

        public T Instantiate<T>(T behaviour, Vector3 position, Transform parent = null) where T : MonoBehaviour =>
            _container.InstantiatePrefab(behaviour, position, Quaternion.identity, parent).GetComponent<T>();

        public T Instantiate<T>(T behaviour, Vector3 position, Quaternion quaternion, Transform parent = null)
            where T : MonoBehaviour =>
            _container.InstantiatePrefab(behaviour, position, quaternion, parent).GetComponent<T>();
    }
}