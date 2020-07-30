using UnityEngine;


namespace Rescues
{
    public sealed class InitializeCharacterController : IInitializeController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public InitializeCharacterController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var resources = Resources.Load<PlayerBehaviour>(AssetsPathGameObject.Object[GameObjectType.Character]);
            var playerData = Data.PlayerData;
            var obj = Object.Instantiate(resources, playerData.Position, Quaternion.identity).transform;
            
            CharacterModel character = new CharacterModel(obj, playerData);
           
            _context.Character = character;
        }

        #endregion
    }
}
