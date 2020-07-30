using System.Collections.Generic;


namespace Rescues
{
    public abstract class Controllers : IInitializeController, IExecuteController, ICleanupController, ITearDownController
    {
        #region Fields
        
        protected readonly List<IInitializeController> _initializeControllers;
        protected readonly List<IExecuteController> _executeControllers;
        protected readonly List<ICleanupController> _cleanupControllers;
        protected readonly List<ITearDownController> _tearDownControllers;

        #endregion 


        #region ClassLifeCycles
        
        protected Controllers()
        {
            _initializeControllers = new List<IInitializeController>();
            _executeControllers = new List<IExecuteController>();
            _cleanupControllers = new List<ICleanupController>();
            _tearDownControllers = new List<ITearDownController>();
        }

        #endregion


        #region Methods

        protected virtual Controllers Add(IController controller)
        {
            if (controller is ICleanupController cleanupController)
            {
                _cleanupControllers.Add(cleanupController);
            }

            if (controller is IExecuteController executeController)
            {
                _executeControllers.Add(executeController);
            }

            if (controller is IInitializeController initializeController)
            {
                _initializeControllers.Add(initializeController);
            }

            if (controller is ITearDownController tearDownController)
            {
                _tearDownControllers.Add(tearDownController);
            }

            return this;
        }

        #endregion


        #region IInitializeController
        
        public virtual void Initialize()
        {
            for (var index = 0; index < _initializeControllers.Count; ++index)
            {
                _initializeControllers[index].Initialize();
            }
        }

        #endregion


        #region IExecuteController
        
        public virtual void Execute()
        {
            for (var index = 0; index < _executeControllers.Count; ++index)
            {
                _executeControllers[index].Execute();
            }
        }

        #endregion


        #region ICleanupController

        public virtual void Cleanup()
        {
            for (var index = 0; index < _cleanupControllers.Count; ++index)
            {
                _cleanupControllers[index].Cleanup();
            }
        }

        #endregion
        

        #region ITearDownController

        public virtual void TearDown()
        {
            for (var index = 0; index < _tearDownControllers.Count; ++index)
            {
                _tearDownControllers[index].TearDown();
            }
        }

        #endregion
    }
}
