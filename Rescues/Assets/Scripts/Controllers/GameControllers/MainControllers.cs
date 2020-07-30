namespace Rescues
{
    public sealed class MainControllers : Controllers
    {
        #region ClassLifeCycles
        
        public MainControllers(GameContext context, Services services)
        {
            Add(new InitializeCharacterController(context, services));
            Add(new InitializeInteractableObjectController(context, services));          
            Add(new InitializeEnemyController(context, services));
            Add(new TimeRemainingController());
            Add(new ItemActiveController(context, services));
            Add(new DoorTeleporterController(context, services));
            Add(new HidingPlaceController(context, services));
            Add(new EnemyVisionController(context, services));
            Add(new EnemyMoveController(context, services));
            Add(new InputController(context, services));
        }

        #endregion
    }
}
