using System;

namespace TimeTracker.Domain
{
    public class WorkTime
    {
        public int Id { get; set; }
        public string Desciption { get; set; }
        public int UserHistory { get; set; }
        public int Task { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
