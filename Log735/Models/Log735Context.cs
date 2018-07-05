namespace Log735
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Log735Context : DbContext
    {
        public Log735Context()
            : base("name=Log735Context")
        {
        }

        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Trimester> Trimesters { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cours>()
                .Property(e => e.CourseAcronym)
                .IsUnicode(false);

            modelBuilder.Entity<Cours>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<Cours>()
                .Property(e => e.ScheduledTime)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Trimester>()
                .Property(e => e.TrimesterName)
                .IsUnicode(false);
        }
    }
}
