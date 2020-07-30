using DG.Tweening;
using UnityEngine;


namespace Rescues
{
    public sealed class DoorTeleporterController : IInitializeController, ITearDownController
    {
        #region Fields
        
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public DoorTeleporterController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController
        
        public void Initialize()
        {
            var doors = _context.GetTriggers(InteractableObjectType.Door);
            foreach (var trigger in doors)
            {
                var doorTeleporterBehaviour = trigger as DoorTeleporterBehaviour;
                doorTeleporterBehaviour.OnFilterHandler += OnFilterHandler;
                doorTeleporterBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                doorTeleporterBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;
            }
        }

        #endregion


        #region ITearDownController
        
        public void TearDown()
        {
            var doors = _context.GetTriggers(InteractableObjectType.Door);
            foreach (var trigger in doors)
            {
                var doorTeleporterBehaviour = trigger as DoorTeleporterBehaviour;
                doorTeleporterBehaviour.OnFilterHandler -= OnFilterHandler;
                doorTeleporterBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                doorTeleporterBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;
            }
        }

        #endregion


        #region Methods
        
        private bool OnFilterHandler(Collider2D obj)
        {
            return obj.CompareTag(TagManager.PLAYER);
        }
        
        private void OnTriggerEnterHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = true;
            var materialColor = enteredObject.GameObject.GetComponent<SpriteRenderer>().color;
            enteredObject.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
                materialColor.g, materialColor.b, 0.5f), 1.0f);
        }

        private void OnTriggerExitHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = false;
            var materialColor = enteredObject.GameObject.GetComponent<SpriteRenderer>().color;
            enteredObject.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
                materialColor.g, materialColor.b, 1.0f), 1.0f);
        }

        #endregion
    }
}
