using UnityEngine;


namespace Rescues
{
    public sealed class EnemyMoveController : IExecuteController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public EnemyMoveController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            //TODO bring all it method to EnemyBehavoiur into method Move()
            if (_context.Enemy.EnemyData.StateEnemy == StateEnemy.Dead)
            {
                // todo Unsubscribe enemy from executing
                return;
            }
            var enemy = _context.Enemy;
            var wayPointInfo = enemy.RouteData.GetWayPoints();
            if (Vector3.Distance(enemy.transform.position, wayPointInfo[enemy.PatrolPointState].PointPosition) > 0.5)
            {
                Vector3 movementDirection = Vector3.zero;
                movementDirection.x = wayPointInfo[enemy.PatrolPointState].PointPosition.x - enemy.transform.position.x;

                _context.Enemy.transform.position += movementDirection.normalized * enemy.EnemyData.Speed * Time.deltaTime;
                _context.Enemy.SetVisionDirection(movementDirection);
            }
            else
            {
                var modificator = enemy.Modificator;
                if(enemy.PatrolPointState + modificator > wayPointInfo.Length - 1 || 
                   enemy.PatrolPointState + modificator < 0)
                {
                    enemy.InvertModificator();
                }
                enemy.WaitTime(wayPointInfo[enemy.PatrolPointState].WaitTime);
            }
        }

        #endregion
    }
}