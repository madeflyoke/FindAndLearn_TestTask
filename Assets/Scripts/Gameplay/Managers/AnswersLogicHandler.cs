using System;
using System.Collections.Generic;
using Gameplay.Creators.Interfaces;
using Gameplay.Data;
using Gameplay.Managers.Interfaces;
using UnityEngine;

namespace Gameplay.Managers
{
    public class AnswersLogicHandler : IAnswersLogicValidator
    {
        public event Action CorrectAnswerDone; 
        
        private ICategoryItemsCreator _itemsCreator;
        private CategoryItemData _correctAnswerData;

        public AnswersLogicHandler(ICategoryItemsCreator itemsCreator)
        {
            _itemsCreator = itemsCreator;
            _itemsCreator.ItemsCreated += SetCorrectAnswer;
        }

        private void SetCorrectAnswer(List<CategoryItemData> itemDatas)
        {
            _correctAnswerData = itemDatas[UnityEngine.Random.Range(0, itemDatas.Count)];
            Debug.LogWarning(_correctAnswerData.Id);
        }

        public bool CallOnValidateAnswer(string answerId)
        {
            if (_correctAnswerData.Id==answerId)
            {
                CorrectAnswerDone?.Invoke();
                return true;
               
            }

            return false;
        }
    }
}
