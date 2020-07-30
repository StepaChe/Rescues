using System.Collections.Generic;


namespace Rescues
{
    public sealed class AssetsPathGameObject
    {
        #region Fields

        public static readonly Dictionary<GameObjectType, string> Object = new Dictionary<GameObjectType, string>()
        {
            { GameObjectType.Character, "Prefabs/Player/Prefabs_Player_Character" },
            { GameObjectType.Enemy, "Prefabs/Enemies/Prefabs_Enemies_Patrolling" },
            { GameObjectType.Canvas, "Prefabs/UI/Prefabs_UI_Canvas" },
        };

        public static readonly Dictionary<ScreenType, string> Screens = new Dictionary<ScreenType, string>()
        {
            { ScreenType.GameOver, "Prefabs/UI/Screen/Prefabs_UI_Screen_GameOver" },
        };

        #endregion
    }
}
