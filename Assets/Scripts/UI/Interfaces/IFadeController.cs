using System;
using UnityEngine;

namespace UI.Interfaces
{
    public interface IFadeController
    {
        public void ShowFade(float fadeAmount =1f, Transform optionalVisibleTarget=null, Action onComplete=null);

        public void HideFade();
    }
}
