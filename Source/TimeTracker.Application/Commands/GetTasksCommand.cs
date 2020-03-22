using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeTracker.Application.Commands
{
    public class GetTasksCommand
    {
        public Task<List<Domain.Task>> Run(int page = 0)
        {
            return Task.FromResult(new List<Domain.Task>());
        }
    }
}
