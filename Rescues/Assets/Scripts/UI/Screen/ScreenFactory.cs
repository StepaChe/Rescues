using UnityEngine;


namespace Rescues
{
    public sealed class ScreenFactory
    {
        #region Fields

        private GameMenuBehaviour _gameMenu;
        private MainMenuBehaviour _mainMenu;
        private GameOverBehaviour _gameOverMenu;
        private Canvas _canvas;

        #endregion


        #region ClassLifeCycles

        public ScreenFactory()
        {
            GetCanvas();
        }

        #endregion


        #region Methods

        public Canvas GetCanvas()
        {
            if (_canvas == null)
            {
                var resources = Resources.Load<Canvas>(AssetsPathGameObject.Object[GameObjectType.Canvas]);
                _canvas = Object.Instantiate(resources, Vector3.one, Quaternion.identity);
            }
            return _canvas;
        }

        public GameMenuBehaviour GetGameMenu()
        {
            if (_gameMenu == null)
            {
                var resources = Resources.Load<GameMenuBehaviour>(AssetsPathGameObject.Screens[ScreenType.GameMenu]);
                _gameMenu = Object.Instantiate(resources, GetCanvas().transform.position, Quaternion.identity, GetCanvas().transform);
            }
            return _gameMenu;
        }

        public MainMenuBehaviour GetMainMenu()
        {
            if (_mainMenu == null)
            {
                var resources = Resources.Load<MainMenuBehaviour>(AssetsPathGameObject.Screens[ScreenType.MainMenu]);
                _mainMenu = Object.Instantiate(resources, GetCanvas().transform.position, Quaternion.identity, GetCanvas().transform);
            }
            return _mainMenu;
        }

        public GameOverBehaviour GetGameOver()
        {
            if (_gameOverMenu == null)
            {
                var resources = Resources.Load<GameOverBehaviour>(AssetsPathGameObject.Screens[ScreenType.GameOver]);
                _gameOverMenu = Object.Instantiate(resources, GetCanvas().transform.position, Quaternion.identity, GetCanvas().transform);
            }
            return _gameOverMenu;
        }

        #endregion
    }
}
