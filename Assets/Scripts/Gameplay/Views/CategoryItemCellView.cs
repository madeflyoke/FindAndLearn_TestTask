using System;
using Gameplay.Data;
using Gameplay.Managers.Interfaces;
using UnityEngine;
using Utils;
using VContainer;

namespace Gameplay.Views
{
    public class CategoryItemCellView : MonoBehaviour
    {
        public Vector2 CellSize => _spriteRenderer.bounds.size;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ElementAnimator _elementAnimator;

        private CategoryItemView _currentItemView;
        private IAnswersLogicValidator _answersLogicValidator;
        private static bool s_correctAnswerDone;
        
        [Inject]
        public void Construct(IAnswersLogicValidator answersLogicValidator)
        {
            _answersLogicValidator = answersLogicValidator;
            s_correctAnswerDone = false;
        }
        
        public void SetRelatedItemView(CategoryItemView itemView)
        {
            _currentItemView = itemView;
        }

        public void Show(bool animated)
        {
            gameObject.SetActive(true);
            if (animated)
            {
                _elementAnimator.PlayBounceEffect();
            }
        }
        
        private void OnMouseDown()
        {
            if (s_correctAnswerDone)
            {
                return;
            }
            var isCorrectAnswer = _answersLogicValidator.CallOnValidateAnswer(_currentItemView.RelatedData.Id);
            s_correctAnswerDone = isCorrectAnswer;
            PlayAnimationOnContent(isCorrectAnswer);
        }

        private void PlayAnimationOnContent(bool isCorrectAnswer)
        {
            if (isCorrectAnswer==false)
            {
                _elementAnimator.PlayShakingEffect(_currentItemView.transform);
            }
            else
            {
                _elementAnimator.PlayBounceEffect(_currentItemView.transform);
            }
        }
    }
}