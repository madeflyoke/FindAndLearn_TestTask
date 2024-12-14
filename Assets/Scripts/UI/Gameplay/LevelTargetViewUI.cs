using DG.Tweening;
using Gameplay.Data;
using TMPro;
using UnityEngine;

namespace UI.Gameplay
{
    public class LevelTargetViewUI : MonoBehaviour
    {
        [SerializeField] private string _prefix;
        [SerializeField] private TMP_Text _targetText;
        [SerializeField] private CanvasGroup _canvasGroup;
        private Tween _tween;

        private void Awake()
        {
            Hide();
        }
        
        public void Show(CategoryItemData targetData, bool animated)
        {
            _targetText.text = _prefix + targetData.Id;
            
            gameObject.SetActive(true);
            if (animated)
            {
                _tween?.Kill(true);
                _canvasGroup.alpha = 0;
                _tween = _canvasGroup.DOFade(1f, 1f).SetEase(Ease.Linear);
            }
            else
            {
                _canvasGroup.alpha = 1;
            }
        }
        
        public void Hide()
        {
            _canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
