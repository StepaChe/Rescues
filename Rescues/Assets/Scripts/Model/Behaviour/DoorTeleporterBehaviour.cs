using UnityEngine;


namespace Rescues
{
    public sealed class DoorTeleporterBehaviour : InteractableObjectBehavior
    {
        #region Fields

        [SerializeField] public Transform ExitPoint;
        [SerializeField] public float TransferTime = 2f;
        private ITrigger _triggerImplementation;

        #endregion
    }
}
