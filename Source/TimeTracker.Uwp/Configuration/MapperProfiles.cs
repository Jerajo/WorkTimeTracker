using AutoMapper;
using TimeTracker.Application.Dtos;
using TimeTracker.Core.ValueObjects;

namespace TimeTracker.Uwp.Configuration
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
            // --------- // GROUP // --------- //
            CreateMap<Domain.Group, GroupDto>();
            CreateMap<GroupDto, Domain.Group>();
            CreateMap<Core.Group, GroupDto>();
            CreateMap<GroupDto, Core.Group>();
            CreateMap<Core.Group, Domain.Group>();
            CreateMap<Domain.Group, Core.Group>();

            // --------- // TASK // --------- //
            CreateMap<Core.Task, TaskDto>();
            CreateMap<TaskDto, Core.Task>();
            CreateMap<Domain.Task, TaskDto>();
            CreateMap<TaskDto, Domain.Task>();
            CreateMap<Core.Task, Domain.Task>();
            CreateMap<Domain.Task, Core.Task>();

            // --------- // TEMPLATE // --------- //
            CreateMap<Description, TaskExportTemplateDto>();
            CreateMap<TaskExportTemplateDto, Description>();

            // --------- // TASKs SCHEDULE // --------- //
            CreateMap<Core.Schedule, ScheduleDto>();
            CreateMap<ScheduleDto, Core.Schedule>();
            CreateMap<Domain.Schedule, ScheduleDto>();
            CreateMap<ScheduleDto, Domain.Schedule>();
            CreateMap<Core.Schedule, Domain.Schedule>();
            CreateMap<Domain.Schedule, Core.Schedule>();

            // --------- // SCHEDULE // --------- //
            CreateMap<Core.TasksSchedule, TasksScheduleDto>();
            CreateMap<TasksScheduleDto, Core.TasksSchedule>();
            CreateMap<Domain.TasksSchedule, TasksScheduleDto>();
            CreateMap<TasksScheduleDto, Domain.TasksSchedule>();
            CreateMap<Core.TasksSchedule, Domain.TasksSchedule>();
            CreateMap<Domain.TasksSchedule, Core.TasksSchedule>();
        }
    }
}
