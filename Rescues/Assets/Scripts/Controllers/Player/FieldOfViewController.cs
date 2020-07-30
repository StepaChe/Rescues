using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public sealed class FieldOfViewController : MonoBehaviour
    {
        #region PrivateData

        [SerializeField] private bool _isFacingRight;
        private BoxCollider2D _box;
        private float _rayLength;
        private PhysicsService _physicsService;
        private readonly GameContext _context;

        #endregion

        #region ClassLifeCycles

        public FieldOfViewController(GameContext context, Services services)
        {
            _context = context;
        }


        #endregion


        #region UnityMethods
        private void Awake()
        {
            _box = GetComponent<BoxCollider2D>();
            _physicsService = Services.SharedInstance.PhysicsService;
            _rayLength = Data.FieldOfViewData.RayLength;
        }


        private void Update()
        {
            if (_isFacingRight) _physicsService.PlayerVision(transform.position, Vector2.right, _rayLength, _box);
            else _physicsService.PlayerVision(transform.position, Vector2.left, _rayLength, _box);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(TagManager.FOGOFWAR))
            {
                var fog = collision.GetComponent<FogOfWarBehaviour>();
                fog.FogEnter();
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(TagManager.FOGOFWAR))
            {
                var fog = collision.GetComponent<FogOfWarBehaviour>();
                if(!fog.IsPlayerEnter)fog.FogLeft();
            }
        }

        #endregion
    }
}
