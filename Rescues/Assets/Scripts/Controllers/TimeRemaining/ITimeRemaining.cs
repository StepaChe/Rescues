using System;


namespace Rescues
{
    public interface ITimeRemaining
    {
        Action Method { get; }
        bool IsRepeating { get; }
        float Time { get; set; }
        float CurrentTime { get; set; }
    }
}
