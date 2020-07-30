using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public sealed class PhysicsService : Service
    {
        #region Fields

        private const int COLLIDED_OBJECT_SIZE = 20;

        private readonly Collider2D[] _collidedObjects;
        private readonly RaycastHit2D[] _castBuffer;
        private readonly List<ITrigger> _triggeredObjects;

        #endregion

        
        #region ClassLifeCycles

        public PhysicsService(Contexts contexts) : base(contexts)
        {
            _collidedObjects = new Collider2D[COLLIDED_OBJECT_SIZE];
            _castBuffer = new RaycastHit2D[64];
            _triggeredObjects = new List<ITrigger>();
        }

        #endregion

        
        #region Methods

        public bool CheckGround(Vector2 position, float distanceRay, out Vector2 hitPoint, int layerMask = LayerManager.DEFAULT_LAYER)
        {
            hitPoint = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, distanceRay, layerMask);
            if (hit.collider == null)
            {
                return false;
            }

            hitPoint = hit.point;
            return true;
        }

        public bool VisionDetectionPlayer(Vector3 rayOriginPosition, Vector3 rayTargetDirection, float detectionDistance)
        {
            int layerMask = LayerManager.PlayerLayer;
            RaycastHit2D hit = Physics2D.Raycast(rayOriginPosition, rayTargetDirection, detectionDistance, layerMask);
            Debug.DrawRay(rayOriginPosition, rayTargetDirection * detectionDistance, Color.red);
            return hit;
        }

        public List<ITrigger> GetObjectsInRadius(Vector2 position, float radius, int layerMask = LayerManager.DEFAULT_LAYER)
        {
            _triggeredObjects.Clear();
            ITrigger trigger;

            int collidersCount = Physics2D.OverlapCircleNonAlloc(position, radius, _collidedObjects, layerMask);
            
            for (int i = 0; i < collidersCount; i++)
            {
                trigger = _collidedObjects[i].GetComponent<ITrigger>();

                if (trigger != null && !_triggeredObjects.Contains(trigger))
                {
                    _triggeredObjects.Add(trigger);
                }
            }

            return _triggeredObjects;
        }
        
        public HashSet<ITrigger> SphereCastObject(Vector2 center, float radius, HashSet<ITrigger> outBuffer,
            int layerMask = LayerManager.DEFAULT_LAYER)
        {
            outBuffer.Clear();

            int hitCount = Physics2D.CircleCastNonAlloc(center,
                radius,
                Vector2.zero,
                _castBuffer,
                0.0f,
                layerMask);

            for (int i = 0; i < hitCount; i++)
            {
                ITrigger carTriggerProvider = _castBuffer[i].collider.GetComponent<ITrigger>();
                if (carTriggerProvider != null)
                {
                    outBuffer.Add(carTriggerProvider);
                }
            }


            return outBuffer;
        }
        
        public ITrigger GetNearestObject(Vector3 targetPosition, HashSet<ITrigger> objectBuffer)
        {
            float nearestDistance = Mathf.Infinity;
            ITrigger result = null;

            foreach (ITrigger trigger in objectBuffer)
            {
                float objectDistance = (targetPosition - trigger.GameObject.transform.position).sqrMagnitude;
                if (objectDistance >= nearestDistance)
                {
                    continue;
                }

                nearestDistance = objectDistance;
                result = trigger;
            }

            return result;
        }

        #endregion
    }
}
