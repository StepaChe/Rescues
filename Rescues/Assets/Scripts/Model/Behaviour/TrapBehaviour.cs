namespace Rescues
{
    public sealed class TrapBehaviour : InteractableObjectBehavior
    {
        public TrapInfo TrapInfo;

        private void Start()
        {
            TrapInfo.BaseTrapData.IsActive = false;
        }

        public void CreateTrap()
        {
            TrapInfo.BaseTrapData.IsActive = true;
            CustomDebug.Log("Trap Active");
        }
    }
}
