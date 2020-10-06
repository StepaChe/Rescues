using System;
using UnityEngine;

namespace Rescues
{
    public class WirePoint : MonoBehaviour
    {
        #region Fileds

        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _enterPoint;
        [SerializeField] private GameObject _exitPoint;

        private Vector3 _enterPointPositionRemeber;
        private Vector3 _exitPointPositionRemeber;

        #endregion


        #region Properties

        public Vector3 EnterPos => _enterPoint.transform.position;
        public Vector3 ExitPos => _exitPoint.transform.position;

        private Vector3 EnterLocalPos
        {
            get => _enterPoint.transform.localPosition;
            set => _enterPoint.transform.localPosition = value;
        }

        private Vector3 ExitLocalPos
        {
            get => _exitPoint.transform.localPosition;
            set => _exitPoint.transform.localPosition = value;
        }

        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public Vector3 LocalPosition
        {
            get => _transform.localPosition;
            set => _transform.localPosition = value;
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Reset()
        {
            _transform = GetComponent<Transform>();
            _enterPointPositionRemeber = _enterPoint.transform.localPosition;
            _exitPointPositionRemeber = _exitPoint.transform.localPosition;
        }

        private void OnValidate()
        {
            
            
            if (!_enterPoint || !_exitPoint)
                throw new NullReferenceException("Missing enter or exit point in WirePoint");
        }

        private void OnDrawGizmos()
        {
            if (_enterPoint && _exitPoint)
            {
                if (_enterPointPositionRemeber != EnterLocalPos)
                {
                    _enterPointPositionRemeber = EnterLocalPos;
                    ExitLocalPos = FlipPosition(EnterLocalPos);
                    _exitPointPositionRemeber = ExitLocalPos;
                }

                if (_exitPointPositionRemeber != ExitLocalPos)
                {
                    _exitPointPositionRemeber = ExitLocalPos;
                    EnterLocalPos = FlipPosition(ExitLocalPos);
                    _enterPointPositionRemeber = EnterLocalPos;
                }

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(_enterPoint.transform.position, _exitPoint.transform.position);
            }
        }

        #endregion


        private Vector3 FlipPosition(Vector3 enterPosition)
        {
            var newPosition = new Vector3(-enterPosition.x, -enterPosition.y, enterPosition.z);
            return newPosition;
        }
    }
}