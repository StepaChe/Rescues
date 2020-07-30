using UnityEngine;


namespace Rescues
{
    public sealed class TooltipController : IController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public TooltipController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region Methods

        private T GetInteractableObject<T>(InteractableObjectType type) where T : class
        {
            var interactableObjects = _context.GetTriggers(type);
            T behaviour = default;

            foreach (var trigger in interactableObjects)
            {
                if (trigger.IsInteractable)
                {
                    CustomDebug.Log(trigger.Description);
                }
            }

            return behaviour;
        }

        #endregion
    }
}