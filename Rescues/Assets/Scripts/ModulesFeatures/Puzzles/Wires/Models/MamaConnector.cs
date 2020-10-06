using System;
using UnityEngine;


namespace Rescues
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MamaConnector : MonoBehaviour
    {
        #region Fileds

        public event Action Connected = () => { };

        [SerializeField] private int _applyNumber;
        [SerializeField] private Sprite _connected;
        [SerializeField] private Sprite _disconnected;
        private int _connectedPapaConnectorHash;
        private SpriteRenderer _spriteRenderer;
        private PapaConnector _papaConnector;

        #endregion


        #region Property

        public bool IsBusy { get; private set; } = false;
        public bool IsCorrectWire { get; private set; } = false;

        public Vector2 Position => transform.position;

        public int ApplyNumber => _applyNumber;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _disconnected;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var newPapaConnector = other.GetComponent<PapaConnector>();
            if (newPapaConnector == null) return;

            if (!IsBusy && !newPapaConnector.IsMoving)
            {
                _papaConnector = newPapaConnector;
                _connectedPapaConnectorHash = _papaConnector.GetHashCode();
                _spriteRenderer.sprite = _connected;
                Connect(_papaConnector.Number);
            }
            else
            {
                if (newPapaConnector.GetHashCode() == _connectedPapaConnectorHash)
                {
                    if (IsBusy && _papaConnector.IsMoving)
                    {
                        Disconnect();
                    }
                }
            }
        }

        #endregion


        #region Methods

        private void Connect(int wireNumber)
        {
            if (IsBusy) return;
            _papaConnector.MoveWire(transform.position);
            _papaConnector.SetSpriteConnector(false);
            IsCorrectWire = _applyNumber == wireNumber ? true : false;
            IsBusy = true;
            Connected.Invoke();
        }

        public void Disconnect()
        {
            if (!IsBusy) return;
            _papaConnector.SetSpriteConnector(true);
            _papaConnector = null;
            _spriteRenderer.sprite = _disconnected;
            _connectedPapaConnectorHash = 0;
            IsCorrectWire = false;
            IsBusy = false;
        }

        #endregion
    }
}