using UnityEngine;


namespace Rescues
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PapaConnector : MonoBehaviour
    {
        #region Fileds

        private const int SORTING_ORDER = 3;

        [SerializeField] private Sprite _connectorSprite;
        private Wire _wire;
        private bool _isMoving = false;
        [SerializeField] private BoxCollider2D _dragbleBounds;
        private SpriteRenderer _spriteRenderer;
        #endregion

        
        #region Properties

        public int Number => _wire.Number;
        public bool IsMoving => _isMoving;

        #endregion


        #region UnityMethods
        
        private void Awake()
        {
            _wire = GetComponentInParent<Wire>();
            _dragbleBounds.enabled = false;

            var rigibody = GetComponent<Rigidbody2D>();
            rigibody.simulated = true;
            rigibody.isKinematic = true;

            var boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sortingOrder = SORTING_ORDER;

            if (_connectorSprite)
                _spriteRenderer.sprite = _connectorSprite;
        }

        private void OnMouseDown()
        {
            _isMoving = true;
            _dragbleBounds.enabled = true;
            _wire.SetEndPointRemeber();
        }

        private void OnMouseDrag()
        {
            var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_dragbleBounds.OverlapPoint(cursorPosition))
                MoveWire(cursorPosition);
        }

        private void OnMouseUp()
        {
            _isMoving = false;
            _dragbleBounds.enabled = false;
        }
        
        #endregion


        #region Methods
        
        public void MoveWire(Vector2 newPosition) => _wire.MoveWire(newPosition);

        public void SetSpriteConnector(bool value) => _spriteRenderer.enabled = value;

        #endregion
    }
}