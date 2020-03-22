namespace Migration.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WorkTimeTracker : DbContext
    {
        public WorkTimeTracker()
            : base("name=WorkTimeTracker")
        {
        }

        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TimeSchedule> TimeSchedules { get; set; }
        public virtual DbSet<UserStory> UserStories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasMany(e => e.TimeSchedules)
                .WithRequired(e => e.Schedule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TimeSchedules)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserStory>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
