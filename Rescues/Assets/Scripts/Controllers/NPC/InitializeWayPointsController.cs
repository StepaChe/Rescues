using UnityEngine;
using System.Linq;
using System;


namespace Rescues
{
    [Obsolete]
    public sealed class InitializeWayPointsController : IInitializeController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public InitializeWayPointsController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            var wayPoints = GameObject.FindGameObjectsWithTag(
                TagManager.WAYPOINT).Select(wayPoint => wayPoint.transform.position).ToArray();

            if(wayPoints.Length < 1)
            {
                throw new Exception("На сцене не установлены объекты wayPoints для ИИ, долбо...");
            }

            //_context.Enemy.WayPoints = wayPoints;
        }

        #endregion
    }
}
