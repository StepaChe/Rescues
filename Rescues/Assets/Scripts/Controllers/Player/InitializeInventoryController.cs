namespace Rescues
{
    public sealed class InitializeInventoryController : IInitializeController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public InitializeInventoryController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {           
            var inventory = new Inventory();
            _context.Inventory = inventory;
        }

        #endregion
    }
}
