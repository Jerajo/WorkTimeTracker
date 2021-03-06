﻿using System.Collections.Generic;

namespace TimeTracker.Core.Contracts
{
    /// <summary>
    /// Represents a group of task or a user story.
    /// </summary>
    public interface IGroup : IEntity
    {
        /// <summary>
        /// Code use in to identify the user story.
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// Name of the group.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Related tasks of the group.
        /// </summary>
        List<Task> Tasks { get; }
    }
}
