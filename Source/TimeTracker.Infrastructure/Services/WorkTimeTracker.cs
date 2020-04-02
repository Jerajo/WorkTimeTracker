using Microsoft.EntityFrameworkCore;
using TimeTracker.Core;
using TimeTracker.Core.Contracts;

namespace TimeTracker.Infrastructure.Services
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

        #endregion

        #region Configuration

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=WorkTimeTracker.db");
                optionsBuilder.UseLazyLoadingProxies();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Schedule>()
                .HasMany(e => e.TasksSchedules)
                .WithOne(e => e.Schedule)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Task>()
                .Property(e => e.Name)
                .IsUnicode();

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TasksSchedules)
                .WithOne(e => e.Task)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Group>()
                .Property(e => e.Name)
                .IsUnicode();
        }

        #endregion
    }
}