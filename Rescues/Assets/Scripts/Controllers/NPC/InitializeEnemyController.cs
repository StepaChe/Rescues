using UnityEngine;


namespace Rescues
{
    public sealed class InitializeEnemyController : IInitializeController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public InitializeEnemyController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            var resources = Resources.Load<EnemyBehaviour>(AssetsPathGameObject.Object[GameObjectType.Enemy]);
            var enemyData = resources.EnemyData;

            var enemyObject = Object.Instantiate(resources, Vector3.zero, Quaternion.identity);
            _context.Enemy = enemyObject;

            var wayPoint = _context.Enemy.RouteData.GetWayPoints()[0];
            enemyObject.transform.position = wayPoint.PointPosition;
        }

        #endregion
    }
}
