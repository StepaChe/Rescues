using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Rescues
{
    public class WiresPuzzle : Puzzle
    {
        #region Fileds
        
        private List<MamaConnector> _connectors = new List<MamaConnector>();
        private Dictionary<int, Vector2> _startPositions = new Dictionary<int, Vector2>();
        private List<WirePoint> _wirePoints = new List<WirePoint>();
        
        #endregion
        
        
        #region  Propeties

        public List<MamaConnector> Connectors => _connectors;
        public Dictionary<int, Vector2> StartPositions => _startPositions;
        public List<WirePoint> WirePoints => _wirePoints;
        
        #endregion


        #region UnityMethods

        private void Awake()
        {
            var connectors = gameObject.GetComponentsInChildren<MamaConnector>();
            foreach (var connector in connectors)
            {
                _connectors.Add(connector);
                // Убери коммент, чтобы првоерять завршен ли пазл каждое присоедениее провода
                //connector.Connected += CheckComplete;
            }
        }

        private void OnEnable()
        {
            if (_startPositions.Count == 0)
            {
                _wirePoints = GetComponentsInChildren<WirePoint>().ToList();
                foreach (var wirePoint in _wirePoints)
                {
                    _startPositions.Add(wirePoint.GetHashCode(), wirePoint.transform.localPosition);
                }
            }
        }

        #endregion
    }
}