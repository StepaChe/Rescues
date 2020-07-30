using UnityEngine;


namespace Rescues
{
    public class WayPointBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RouteData _routeData; 
        [SerializeField] private float _waitTime;
        [SerializeField] private TrapBehaviour _activatingTrap;
        [SerializeField] private BaseTrapData _baseTrapData;

        #endregion

        #region Properte

        public TrapBehaviour ActivatingTrap => _activatingTrap;

        #endregion

        private void Awake()
        {
            FindObjects();
        }

        private void FindObjects()
        {
            var wayPointBehaviours = FindObjectsOfType<WayPointBehaviour>();

            for (int i = 0; i < wayPointBehaviours.Length; i++)
            {
                // if wayPointBehaviours[i].GetRouteData() != _routeData} delete
            }
            // sort wayPointBehaviours[i].numOfWayPoint

            _routeData.SetWayPoints(wayPointBehaviours);
            CustomDebug.Log("Scanned for: " + wayPointBehaviours.Length + " objects");
        }

        public float GetWaitTime()
        {
            return _waitTime;
        }
    }
}