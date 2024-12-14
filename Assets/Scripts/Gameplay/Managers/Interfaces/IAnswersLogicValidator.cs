using System;

namespace Gameplay.Managers.Interfaces
{
    public interface IAnswersLogicValidator
    {
        public event Action CorrectAnswerDone;
        public bool CallOnValidateAnswer(string answerId);
    }
}
