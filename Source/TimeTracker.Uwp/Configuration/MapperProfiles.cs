﻿using AutoMapper;
using TimeTracker.Application.Dtos;

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
            CreateMap<Domain.Task, TaskDto>();
            CreateMap<TaskDto, Domain.Task>();

            CreateMap<Core.Task, TaskDto>();
            CreateMap<TaskDto, Core.Task>();
        }
    }
}
