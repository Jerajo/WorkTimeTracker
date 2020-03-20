using System;
using TimeTracker.Domain;

namespace TimeTracker.Application
{
    public class TrackWorkTimeCommand
    {
        public void Run(WorkTime workTime)
        {
            workTime.Time = DateTimeOffset.Now;
        }
    }
}
