using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeTracker.Application.Commands
{
    public class GetGroupsCommand
    {
        public Task<List<Domain.Group>> Run(int page = 0)
        {
            return Task.FromResult(new List<Domain.Group>());
        }
    }
}
