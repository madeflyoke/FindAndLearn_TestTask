using System;
using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class ElementAnimator : MonoBehaviour
    {
        private Tween _tween;
        private readonly Vector3 _shakingStrength = new Vector3(.2f,0,0); //TODO yes yes config
        private readonly Vector3 _punchStrength = Vector3.one * .2f; 
        
        public void PlayShakingEffect(Transform optionalTarget =null)
        {
            InitializeEffect(ref optionalTarget);
            _tween = optionalTarget.DOShakePosition(.5f, _shakingStrength, 30,
                randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(Ease.Linear);
        }

        public void PlayBounceEffect(Transform optionalTarget =null)
        {
            InitializeEffect(ref optionalTarget);
            _tween = optionalTarget.DOPunchScale(_punchStrength, .5f, vibrato: 7).SetEase(Ease.OutBack);
        }
        
        private void InitializeEffect(ref Transform optionalTarget)
        {
            _tween?.Kill(true);
            optionalTarget = optionalTarget==null ? transform : optionalTarget;
        }

        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
