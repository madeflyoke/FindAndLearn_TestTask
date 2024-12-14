using System;
using System.Collections.Generic;
using Configs;
using Gameplay.Data;
using Gameplay.Levels.Data;
using Gameplay.Managers.Interfaces;
using UnityEngine;

namespace Gameplay.Managers
{
    public class AnswersLogicHandler : MonoBehaviour, IAnswersLogicValidator, IAnswersCreator
    {
        public event Action CorrectAnswerDone;

        [SerializeField] private CategoriesContainer _categoriesContainer;
        private readonly HashSet<CategoryItemData> _previousCorrectAnswers = new HashSet<CategoryItemData>();
        private CategoryItemData _currentCorrectAnswerData;

        public List<CategoryItemData> Create(LevelData levelData, out CategoryItemData correctAnswer)
        {
            var count = levelData.GridSize.x * levelData.GridSize.y;

            var items = _categoriesContainer.GetConfig(levelData.Category)
                .GetRandomItemsDataExcept(count, _previousCorrectAnswers, out correctAnswer);
            _currentCorrectAnswerData = correctAnswer;
            _previousCorrectAnswers.Add(_currentCorrectAnswerData);
            return items;
        }
        
        public bool CallOnValidateAnswer(string answerId)
        {
            if (_currentCorrectAnswerData.Id == answerId)
            {
                CorrectAnswerDone?.Invoke();
                return true;
            }

            return false;
        }
    }
}