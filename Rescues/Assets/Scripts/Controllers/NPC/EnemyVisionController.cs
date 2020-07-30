

namespace Rescues
{
    public sealed class EnemyVisionController : IExecuteController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public EnemyVisionController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            if (_context.Enemy.EnemyData.StateEnemy != StateEnemy.Dead)
            {
                _context.Enemy.Vision();
            }
        }


        #endregion
    }
}
