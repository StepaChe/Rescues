using UnityEngine;


namespace Rescues
{
    public class RaycastTestBehaviour : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float detectionDistance;

        // Update is called once per frame
        void Update()
        {
            int layerMask = _layerMask;
            print(layerMask);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionDistance, layerMask);
            Debug.DrawRay(transform.position, transform.right * detectionDistance, Color.red);

            if(hit) Debug.Log(hit.collider.name);
        }
    }
}
