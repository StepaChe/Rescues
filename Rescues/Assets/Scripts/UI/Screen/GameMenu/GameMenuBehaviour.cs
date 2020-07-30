using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public sealed class GameMenuBehaviour : BaseUi
    {
        #region Fields
        
        [SerializeField] private Button _button;
        
        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _button.onClick.AddListener(Call);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Call);
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

        private void Call()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.MainMenu);
        }

        #endregion
    }
}
