using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UIFadeController : MonoBehaviour
    {
        [SerializeField] private float _duration =1f;
        [SerializeField] private CanvasGroup _canvasGroup;
        private Transform _originalParent;
        private Tween _tween;

        public void Initialize()
        {
            _originalParent = transform.parent;
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
        
        public void Show(float amount, Action onComplete)
        {
            _canvasGroup.blocksRaycasts = true;
            _tween?.Kill(true);
            _tween = _canvasGroup.DOFade(amount, _duration).SetEase(Ease.Linear).OnComplete(()=>onComplete?.Invoke());
        }

        public void Hide()
        {
            _tween?.Kill(true);
            _tween = _canvasGroup.DOFade(0, _duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                _canvasGroup.blocksRaycasts = false;
            });
        }

        public void ResetParent()
        {
            transform.SetParent(_originalParent);
        }

        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
