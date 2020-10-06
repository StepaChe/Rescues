using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Rescues
{
    public class Wire : MonoBehaviour
    {
        #region Fileds

        [SerializeField] private int _number;
        [SerializeField] private PapaConnector _papaConnector;
        [SerializeField] private List<WirePoint> _points;
        [Space] [Header("Wire visual")] 
        [SerializeField, Range(0.01f, 0.5f)] private float _resolution = 0.0166f;
        [Space] [Header("Wire drag settings")] 
        [SerializeField] private float _middleXDividder = 2;
        [SerializeField] private float _middleYDividder = 2;
        [SerializeField] private float _deltaDividder = 2;

        private LineRenderer _lineRenderer;
        private Vector2 _endPointRemeber;
        private bool _canDraw;
        
        #endregion


        #region Properties

        public int Number => _number;

        #endregion


        #region UnityMethods

        private void OnDrawGizmos()
        {
            if (_points.Count > 0)
            {
                var drawPoints = GetDrawingPoints();
                for (int i = 0; i < drawPoints.Count; i++)
                {
                    if (i == drawPoints.Count - 1) continue;
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(drawPoints[i], drawPoints[i + 1]);
                }
            }
        }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            var arrayPoints = GetDrawingPoints().ToArray();
            _lineRenderer.positionCount = arrayPoints.Length;
            DrawWire();
        }

        private void Update()
        {
            _canDraw = _papaConnector.IsMoving ||
                       _points.Last(a => a.transform.position != _papaConnector.transform.position);
            
            if (_canDraw)
                DrawWire();
        }

        #endregion

        
        #region Methods
        
        private List<Vector3> GetDrawingPoints()
        {
            List<Vector3> drawingPoints = new List<Vector3>();
            for (int i = 0; i < _points.Count; i++)
            {
                if (i == _points.Count - 1) continue;

                for (float resolutionParts = 0; resolutionParts < 1; resolutionParts += _resolution)
                {
                    var p1 = Vector2.Lerp(_points[i].Position, _points[i].ExitPos, resolutionParts);
                    var p2 = Vector2.Lerp(_points[i].ExitPos, _points[i + 1].EnterPos, resolutionParts);
                    var p3 = Vector2.Lerp(_points[i + 1].EnterPos, _points[i + 1].Position, resolutionParts);

                    var p12 = Vector2.Lerp(p1, p2, resolutionParts);
                    var p23 = Vector2.Lerp(p2, p3, resolutionParts);

                    var p4 = Vector2.Lerp(p12, p23, resolutionParts);
                    drawingPoints.Add(p4);
                }
            }

            return drawingPoints;
        }
        
        public void SetEndPointRemeber()
        {
            _endPointRemeber = _points[_points.Count - 1].Position;
        }

        public void MoveWire(Vector2 newPosition)
        {
            _points[_points.Count - 1].Position = newPosition;
            var positionDelta = newPosition - _endPointRemeber;

            for (int i = _points.Count - 2; i >= 0; i--)
            {
                if (i == 0)
                    continue;

                var pointPosition = _points[i].Position;
                pointPosition.x += positionDelta.x / _middleXDividder;
                pointPosition.y += positionDelta.y / _middleYDividder;
                _points[i].Position = pointPosition;
                positionDelta /= _deltaDividder;
            }

            _endPointRemeber = newPosition;
        }

        private void DrawWire()
        {
            Vector3[] arrayPoints = GetDrawingPoints().ToArray();
            _lineRenderer.SetPositions(arrayPoints);
        }
        
        #endregion
    }
}