using System.Collections.Generic;


namespace Rescues
{
    public sealed class TimeRemainingCleanUp : ICleanupController
    {      
        #region Fields
                   
        private readonly List<ITimeRemaining> _timeRemainings;
                   
        #endregion
           
                   
        #region ClassLifeCycles
           
        public TimeRemainingCleanUp()
        {
            _timeRemainings = TimeRemainingExtensions.TimeRemainings;
        }
                   
        #endregion
        
        
        #region ICleanupController
        
        public void Cleanup()
        {
            _timeRemainings.Clear();
        }

        #endregion
    }
}