using System;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class EndGamePopup : MonoBehaviour
    {
        public event Action RestartRequired;
        
        [Range(0, 1), SerializeField] private float _fadeAmount;
        [SerializeField] private Button _restartButton;
        private IFadeController _fadeController;
        
        public void Initialize(IFadeController fadeController)
        {
            _fadeController = fadeController;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _fadeController.ShowFade(_fadeAmount, transform);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
           _restartButton.onClick.AddListener(()=>RestartRequired?.Invoke());
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}
