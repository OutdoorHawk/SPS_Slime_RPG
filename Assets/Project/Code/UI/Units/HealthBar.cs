using UnityEngine;
using UnityEngine.UI;

namespace Project.Code.UI.Units
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Vector2 _offsetTest;
        [SerializeField] private Image _fullbarImage;
        private Image[] _images;
        private RectTransform _targetCanvas;

        private RectTransform _healthBarRectTransform;
        private Transform _objectToFollow;

        private Camera _mainCamera;

        public void Init()
        {
            _healthBarRectTransform = GetComponent<RectTransform>();
            _mainCamera = Camera.main;
            _images = GetComponentsInChildren<Image>();
        }

        public void SetTargetToFollow(Transform targetTransform, RectTransform targetCanvas)
        {
            _targetCanvas = targetCanvas;
            _objectToFollow = targetTransform;
            transform.SetParent(_targetCanvas, false);
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_targetCanvas != null)
                RepositionHealthBar();
        }

        public void UpdateHealth(float healthPercent)
        {
            if (_fullbarImage != null)
                _fullbarImage.fillAmount = healthPercent;
        }

        public void SetVisible(bool visible)
        {
            foreach (var item in _images)
                if (item != null)
                    item.enabled = visible;
        }

        private void RepositionHealthBar()
        {
            if (_objectToFollow == null)
                Destroy(gameObject);
            else
            {
                Vector2 screenPoint = _mainCamera.WorldToViewportPoint(_objectToFollow.position);

                // Vector2 screenPosition = new Vector2(
                //  ((targetPosition.x * _targetCanvas.sizeDelta.x) - (_targetCanvas.sizeDelta.x * 0.5f)),
                //  ((targetPosition.y * _targetCanvas.sizeDelta.y) - (_targetCanvas.sizeDelta.y * 0.5f)));


                _healthBarRectTransform.anchoredPosition = (screenPoint - _targetCanvas.sizeDelta / 2f) * 1000 + _offsetTest;
            }
        }
    }
}