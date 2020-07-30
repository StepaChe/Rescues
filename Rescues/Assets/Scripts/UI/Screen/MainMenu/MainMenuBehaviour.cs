using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public sealed class MainMenuBehaviour : BaseUi
    {
        #region Fields
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Text _currentLevelLabel;
        
        private LocationService _locationService;
        
        #endregion 


        #region UnityMethods

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGameButtonClick);
            _settingsButton.onClick.AddListener(ShowSettingsButtonClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGameButtonClick);
            _settingsButton.onClick.RemoveListener(ShowSettingsButtonClick);
        }

        #endregion
        
        
        #region Methods

        public override void Show()
        {
            gameObject.SetActive(true);
            ShowUI.Invoke();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            HideUI.Invoke();
        }

        private void StartGameButtonClick()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.GameMenu);
        }

        private void ShowSettingsButtonClick()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.Settings);
        }

        #endregion
    }
}
