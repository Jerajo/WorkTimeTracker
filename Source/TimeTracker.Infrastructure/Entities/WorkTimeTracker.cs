using Microsoft.EntityFrameworkCore;
using Migration.Entities;

namespace TimeTracker.Infrastructure.Entities
{
    public partial class WorkTimeTracker : DbContext
    {

        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TimeSchedule> TimeSchedules { get; set; }
        public virtual DbSet<UserStory> UserStories { get; set; }

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
                .HasMany(e => e.TimeSchedules)
                .WithOne(e => e.Schedule)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Task>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TimeSchedules)
                .WithOne(e => e.Task)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserStory>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
