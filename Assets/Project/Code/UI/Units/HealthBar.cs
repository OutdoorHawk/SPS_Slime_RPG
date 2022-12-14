using DG.Tweening;
using Project.Code.Infrastructure.Services.UpdateBehavior;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Code.UI.Units
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Vector2 _offsetTest;
        [SerializeField] private Image _fullbarImage;
        [SerializeField] private Image _tweenBarImage;
        [SerializeField] private float _tweenBarDuration = 0.35f;
        [SerializeField] private TMP_Text _healthText;
        
        private RectTransform _targetCanvas;
        private RectTransform _healthBarRectTransform;
        private Transform _objectToFollow;

        private Camera _mainCamera;
        private Tween _tweenBarDamage;
        private Tween _tweenBarHeal;
        private IUpdateBehaviourService _updateBehaviourService;

        public void Init(IUpdateBehaviourService updateBehaviourService)
        {
            _updateBehaviourService = updateBehaviourService;
            _healthBarRectTransform = GetComponent<RectTransform>();
            _mainCamera = Camera.main;
            if (_healthText!= null) 
                _healthText.gameObject.SetActive(true);
        }

        public void SetTargetToFollow(Transform targetTransform, RectTransform targetCanvas)
        {
            _targetCanvas = targetCanvas;
            _objectToFollow = targetTransform;
            transform.SetParent(_targetCanvas, false);
            gameObject.SetActive(true);
            _tweenBarImage.fillAmount = 1;
            _updateBehaviourService.UpdateEvent += Tick;
        }

        private void Tick()
        {
            if (_targetCanvas != null && _mainCamera != null)
                RepositionHealthBar();
        }

        public void UpdateHealth(float healthPercent)
        {
            if (healthPercent > _fullbarImage.fillAmount)
                DoHealTween(healthPercent);
            else
                DoDamageTween(healthPercent);
        }

        public void UpdateHealthText(float health)
        {
            if (_healthText!= null) 
                _healthText.text = health.ToString("0");
        }

        private void DoDamageTween(float healthPercent)
        {
            UpdateFillAmount(healthPercent);
            _tweenBarDamage?.Kill();
            _tweenBarDamage = _tweenBarImage.DOFillAmount(healthPercent, _tweenBarDuration);
        }

        private void DoHealTween(float healthPercent)
        {
            _tweenBarDamage?.Kill();
            _tweenBarDamage = _tweenBarImage.DOFillAmount(healthPercent, _tweenBarDuration)
                .OnComplete(() => UpdateFillAmount(healthPercent));
        }

        private void UpdateFillAmount(float healthPercent)
        {
            _fullbarImage.fillAmount = healthPercent;
        }

        private void RepositionHealthBar()
        {
            if (_objectToFollow == null)
                Destroy(gameObject);
            else
            {
                Vector2 screenPoint = _mainCamera.WorldToViewportPoint(_objectToFollow.position);

                _healthBarRectTransform.anchoredPosition =
                    (screenPoint - _targetCanvas.sizeDelta / 2f) * 1000 + _offsetTest;
            }
        }

        private void OnDestroy()
        {
            _tweenBarDamage?.Kill();
            _updateBehaviourService.UpdateEvent -= Tick;
        }
    }
}