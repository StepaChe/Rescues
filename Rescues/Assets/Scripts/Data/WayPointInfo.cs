using UnityEngine;
using System;


namespace Rescues
{
    [Serializable]
    public struct WayPointInfo
    {
        public TrapInfo TrapInfo;
        public Vector3 PointPosition;
        public float WaitTime;
    }
}
