using UI.Interfaces;
using UnityEngine;

namespace UI.Gameplay
{
    public class GameplayScreen : MonoBehaviour, IScreen
    {
        [field: SerializeField] public LevelTargetViewUI TargetView { get; private set; }
        [field: SerializeField] public EndGamePopup EndGamePopup  { get; private set; }
        
        public void Initialize(IFadeController fadeController)
        {
            EndGamePopup.Initialize(fadeController);
        }
        
        public void Show()
        {
           gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            TargetView.Hide();
            EndGamePopup.Hide();
        }
    }
}
