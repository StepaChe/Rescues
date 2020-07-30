using UnityEngine;


namespace Rescues
{
    public sealed class InitializeInteractableObjectController : IInitializeController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public InitializeInteractableObjectController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var triggers = Object.FindObjectsOfType<InteractableObjectBehavior>();

            foreach (var trigger in triggers)
            {
                _context.AddTriggers(trigger.Type, trigger);
            }
        }

        #endregion
    }
}
