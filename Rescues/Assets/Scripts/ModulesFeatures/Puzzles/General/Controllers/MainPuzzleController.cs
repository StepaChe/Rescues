using UnityEngine;


namespace Rescues
{
    public sealed class MainPuzzleController : IInitializeController, ITearDownController
    {
        #region Fields

        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public MainPuzzleController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            var puzzleInteracts = _context.GetTriggers(InteractableObjectType.Puzzle);
            var mainPuzzleParent = new GameObject("Puzzles");
            var puzzleControllers = new PuzzlesControllers();

            foreach (var trigger in puzzleInteracts)
            {
                var puzzleBehaviour = trigger as PuzzleBehaviour;
                puzzleBehaviour.OnFilterHandler += OnFilterHandler;
                puzzleBehaviour.OnTriggerEnterHandler += OnTriggerEnterHandler;
                puzzleBehaviour.OnTriggerExitHandler += OnTriggerExitHandler;

                foreach (var somePuzzleController in puzzleControllers.ControllersList)
                {
                    if (somePuzzleController.Value == puzzleBehaviour.Puzzle.GetType())
                    {
                        var puzzleInstance = GameObject.Instantiate(puzzleBehaviour.Puzzle, mainPuzzleParent.transform);
                        
                        puzzleBehaviour.Puzzle = puzzleInstance;
                        somePuzzleController.Key.Initialize(puzzleInstance);
                    }
                }
            }
        }

        #endregion


        #region ITearDownController

        public void TearDown()
        {
            var puzzles = _context.GetTriggers(InteractableObjectType.Puzzle);
            foreach (var trigger in puzzles)
            {
                var puzzleBehaviour = trigger as PuzzleBehaviour;
                puzzleBehaviour.OnFilterHandler -= OnFilterHandler;
                puzzleBehaviour.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                puzzleBehaviour.OnTriggerExitHandler -= OnTriggerExitHandler;
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
        }

        private void OnTriggerExitHandler(ITrigger enteredObject)
        {
            enteredObject.IsInteractable = false;
        }

        #endregion
    }
}