using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Project.Code.Runtime.Units.Components.Animation
{
    public class HitColorComponent : MonoBehaviour
    {
        [SerializeField] private Color32 _hitColor;
        [SerializeField] private float _hitTime = 0.25f;

        private Renderer[] _renderers;
        private Material[] _cachedMaterials;
        private Color[] _defaultColors;

        private Tween _colorTween;
        private IEnumerator _hitColorRoutine;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<Renderer>();
            InitCachedMaterials();
            InitDefaultColors();
        }

        private void InitCachedMaterials()
        {
            _cachedMaterials = new Material[_renderers.Length];
            for (var i = 0; i < _renderers.Length; i++) 
                _cachedMaterials[i] = _renderers[i].material;
        }

        private void InitDefaultColors()
        {
            _defaultColors = new Color[_cachedMaterials.Length];
            for (var i = 0; i < _cachedMaterials.Length; i++)
                _defaultColors[i] = _cachedMaterials[i].color;
        }

        public void DoHitFlash()
        {
            if (_hitColorRoutine != null) 
                StopCoroutine(_hitColorRoutine);
            _hitColorRoutine = HitColorRoutine();
            StartCoroutine(_hitColorRoutine);
        }

        private IEnumerator HitColorRoutine()
        {
            SetHitColor();
            yield return new WaitForSeconds(_hitTime);
            SetDefaultColor();
        }

        private void SetDefaultColor()
        {
            for (var i = 0; i < _cachedMaterials.Length; i++)
                _cachedMaterials[i].color = _defaultColors[i];
        }

        private void SetHitColor()
        {
            foreach (var mat in _cachedMaterials)
                mat.color = _hitColor;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _cachedMaterials.Length; i++) 
                Destroy(_cachedMaterials[i]);
        }
    }
}