using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rescues
{
    public class FogOfWarBehaviour : MonoBehaviour
    {
        #region PrivateData

        [SerializeField] private Color _visibleColor = new Color(255, 255, 255, 0);
        [SerializeField] private Color _hiddenColor = new Color(1f, 1f, 1f, 0.5f);
        [SerializeField] private Color _startColor = new Color(0, 0, 0, 255);
        private SpriteMask _mask;
        private Renderer _renderer;
        private bool _isPlayerEnter;

        #endregion


        #region Fields

        public bool IsPlayerEnter => _isPlayerEnter;

        #endregion


        #region UnityMethods
        void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.material.color = _startColor;
            _mask = GetComponentInChildren<SpriteMask>();
            _mask.enabled = false;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag(TagManager.PLAYER))
            {
                _isPlayerEnter = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag(TagManager.PLAYER))
            {
                _isPlayerEnter = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag(TagManager.PLAYER))
            {
                if (!_mask.enabled) FogEnter();
            }
        }

        #endregion


        #region Methods

        public void FogEnter()
        {
            _renderer.material.color = _visibleColor;
            _mask.enabled = true;
        }

        public void FogLeft()
        {
            _renderer.material.color = _hiddenColor;
            _mask.enabled = false;
        }

        #endregion

    }
}
