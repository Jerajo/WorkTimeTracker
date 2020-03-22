using Microsoft.AspNetCore.Mvc;
using TimeTracker.Application;
using TimeTracker.Domain;

namespace TimeTracker.Api.Controller
{
    [Route("api/worktime")]
    public class WorkTimeController : ControllerBase
    {
        //public WorkTimeController(IRepository<WorkTime> repositoryFactory,
        //    IUnitOfWork unitOfWork,
        //    IMapper mapper,
        //    ICommandFactory commandFactory) { }

        [HttpGet("setTimeNow")]
        public void SetWorkTimeNow()
        {
            var workTime = new Task();
            var trackWorkTimeCommand = new TrackWorkTimeCommand();

            trackWorkTimeCommand.Run(workTime);
        }
    }
}
