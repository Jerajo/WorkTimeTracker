using System;
using TimeTracker.Domain;

namespace TimeTracker.Application
{
    public class TrackWorkTimeCommand
    {
        public void Run(Task workTime)
        {
            throw new NotImplementedException();
            //workTime.Time = DateTimeOffset.Now;
        }
    }
}
