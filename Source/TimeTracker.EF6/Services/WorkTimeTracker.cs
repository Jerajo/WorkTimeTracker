using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Core;
using TimeTracker.Core.Contracts;
using TimeTracker.Core.ValueObjects;
using AsyncOperation = System.Threading.Tasks.Task;
using Task = TimeTracker.Core.Task;

namespace TimeTracker.EF6.Services
{
    public class WorkTimeTracker : DbContext, IDbContext
    {
        public WorkTimeTracker() { }

        public WorkTimeTracker(DbContextOptions<WorkTimeTracker> options) : base(options) { }

        #region Entities

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<TasksSchedule> TasksSchedules { get; set; }

        public virtual DbSet<Schedule> Schedules { get; set; }

        public virtual DbSet<Description> Descriptions { get; set; }

        #endregion

        #region Configuration

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var option = optionsBuilder.Options;
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=WorkTimeTracker.db");
                optionsBuilder.UseLazyLoadingProxies();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .Property(e => e.Name)
                .IsUnicode();

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Schedule>()
                .HasMany(e => e.TasksSchedules)
                .WithOne(e => e.Schedule)
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Task>()
                .Property(e => e.Name)
                .IsUnicode();

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TasksSchedules)
                .WithOne(e => e.Task)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Task>()
                .HasOne(e => e.Description)
                .WithMany()
                .HasForeignKey(e => e.DescriptionId);
        }

        #endregion

        #region Interface Methods

        public Task<bool> EnsureCreated()
        {
            return AsyncOperation.FromResult(Database.EnsureCreated());
        }

        public Task<bool> EnsureDeleted()
        {
            return AsyncOperation.FromResult(Database.EnsureDeleted());
        }

        public Task<int> FetchInitialData()
        {
            Set<Description>().AddRange(
                new List<Description>
                {
                    new Description { Id = 1, Template = "Sprint Planning {0}." },
                    new Description { Id = 2, Template = "Sprint Review {0}." },
                    new Description { Id = 3, Template = "Sprint Retrospective {0}." },
                    new Description { Id = 4, Template = "Sesión de trabajo realizando la programación necesaria para cumplir con el requerimiento asignado, ejecutando la tarea, {0}." },
                });

            return SaveChangesAsync();
        }

        #endregion
    }
}
