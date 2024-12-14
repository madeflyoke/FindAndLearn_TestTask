using System;
using System.Collections.Generic;
using UI.Gameplay;
using UI.Interfaces;
using UnityEngine;

namespace UI
{
    public class ScreensController : MonoBehaviour, IScreenController, IFadeController
    {
        [SerializeField] private GameplayScreen _gameplayScreen;
        [SerializeField] private UIFadeController _uiFadeController;
        private readonly Dictionary<Type, IScreen> _screens = new Dictionary<Type, IScreen>();
        private IScreen _activeScreen;
        
        private void Awake()
        {
            _uiFadeController.Initialize();
            InitializeScreens();
        }

        private void InitializeScreens()
        {
            _screens.Add(typeof(GameplayScreen), _gameplayScreen);
            
            _gameplayScreen.Initialize(this);
            _gameplayScreen.Hide();
        }

        public void ShowScreen<T>() where T : IScreen
        {
            _activeScreen?.Hide();
            _activeScreen = _screens[typeof(T)];
            _activeScreen.Show();
        }

        public T GetScreen<T>() where T : IScreen
        {
            return (T) _screens[typeof(T)];
        }
        
        public void ShowFade(float fadeAmount, Transform optionalVisibleTarget, Action onComplete = null)
        {
            if (optionalVisibleTarget)
            {
                _uiFadeController.transform.SetParent(optionalVisibleTarget.parent);
                _uiFadeController.transform.SetSiblingIndex(optionalVisibleTarget.GetSiblingIndex());
            }
            else
            {
                _uiFadeController.ResetParent();
            }
            _uiFadeController.Show(fadeAmount, onComplete);
        }

        public void HideFade()
        {
            _uiFadeController.Hide();
        }
    }
}
