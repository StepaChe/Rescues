using System;


namespace Rescues
{
    public sealed class Timer
    {
        #region Fields

        private DateTime _start;
        private float _elapsed = -1;
        private TimeSpan _duration;

        #endregion


        #region Methods

        public void StartTimer(float elapsed)
        {
            _elapsed = elapsed;
            _start = DateTime.Now;
            _duration = TimeSpan.Zero;
        }

        public void UpdateTimer()
        {
            if (_elapsed > 0)
            {
                _duration = DateTime.Now - _start;
                if (_duration.TotalSeconds > _elapsed)
                {
                    _elapsed = 0;
                }
            }
            else if (_elapsed == 0)
            {
                _elapsed = -1;
            }
        }

        public bool IsEvent()
        {
            return _elapsed == 0;
        }

        #endregion
    }
}
