using System;
using System.Collections.Generic;


namespace Rescues
{
    public sealed class PuzzlesControllers
    {
        #region Fileds

        private Dictionary<IPuzzleController, Type> _controllersList = new Dictionary<IPuzzleController, Type>();

        #endregion


        #region Properties

        public Dictionary<IPuzzleController, Type> ControllersList => _controllersList;

        #endregion


        #region PrivateData

        /// <summary>
        /// Add here controller your puzzle
        /// </summary>
        public PuzzlesControllers()
        {
            _controllersList.Add(new WiresController(), typeof(WiresPuzzle));
        }

        #endregion
    }
}