using UnityEngine;

namespace Rescues
{
    [CreateAssetMenu(fileName = "RouteData", menuName = "Data/NPC/RouteData")]
    public sealed class RouteData : ScriptableObject
    {
        [SerializeField] private WayPointInfo[] _wayPoints;

        public void SetWayPoints(WayPointBehaviour[] wayPointBehaviours)
        {
            _wayPoints = new WayPointInfo[wayPointBehaviours.Length];

            for (int i = 0; i < wayPointBehaviours.Length; i++)
            {
                _wayPoints[i].PointPosition = wayPointBehaviours[i].transform.position;
                _wayPoints[i].WaitTime = wayPointBehaviours[i].GetWaitTime();
                var activatingTrap = wayPointBehaviours[i].ActivatingTrap;
                if (activatingTrap != null)
                {
                    _wayPoints[i].TrapInfo = activatingTrap.TrapInfo;
                }
            }
        }

        public WayPointInfo[] GetWayPoints()
        {
            return _wayPoints;
        }
    }
}