using AutoMapper;
using TimeTracker.Application.Dtos;

namespace TimeTracker.Tests.Common.Helpers
{
    /// <summary>
    /// Holds all mapper configuration.
    /// </summary>
    public class MapperProfiles : Profile
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MapperProfiles()
        {
            // --------- // DOMAIN - DTO // --------- //
            CreateMap<Domain.Task, TaskDto>();
            CreateMap<TaskDto, Domain.Task>();

            // --------- // CORE - DTO // --------- //
            CreateMap<Core.Task, TaskDto>();
            CreateMap<TaskDto, Core.Task>();

            // --------- // CORE - DOMAIN // --------- //
            CreateMap<Core.Task, Domain.Task>();
            CreateMap<Domain.Task, Core.Task>();
        }
    }
}
