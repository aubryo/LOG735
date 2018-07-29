namespace PrivateRoomDomain.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LOG735Context : DbContext
    {
        public LOG735Context()
            : base("name=LOG735Context")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<ActiveRooms> ActiveRooms { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CourseInfo> CourseInfo { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<PrivateRooms> PrivateRooms { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
        public virtual DbSet<Trimesters> Trimesters { get; set; }
        public virtual DbSet<UserProfiles> UserProfiles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<CourseInfo>()
                .Property(e => e.StartTime)
                .IsUnicode(false);

            modelBuilder.Entity<CourseInfo>()
                .Property(e => e.EndTime)
                .IsUnicode(false);

            modelBuilder.Entity<CourseInfo>()
                .Property(e => e.WDay)
                .IsUnicode(false);

            modelBuilder.Entity<Courses>()
                .Property(e => e.CourseAcronym)
                .IsUnicode(false);

            modelBuilder.Entity<Courses>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<Courses>()
                .HasMany(e => e.CourseInfo)
                .WithRequired(e => e.Courses)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Courses>()
                .HasMany(e => e.Schedules)
                .WithMany(e => e.Courses)
                .Map(m => m.ToTable("ScheduleCourse").MapLeftKey("CourseId").MapRightKey("ScheduleId"));

            modelBuilder.Entity<Departments>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Departments>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Departments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Departments>()
                .HasMany(e => e.UserProfiles)
                .WithRequired(e => e.Departments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrivateRooms>()
                .Property(e => e.RoomName)
                .IsUnicode(false);

            modelBuilder.Entity<PrivateRooms>()
                .Property(e => e.RoomPassword)
                .IsUnicode(false);

            modelBuilder.Entity<PrivateRooms>()
                .HasOptional(e => e.ActiveRooms)
                .WithRequired(e => e.PrivateRooms);

            modelBuilder.Entity<PrivateRooms>()
                .HasMany(e => e.Users1)
                .WithMany(e => e.PrivateRooms1)
                .Map(m => m.ToTable("UserRoom").MapLeftKey("RoomId").MapRightKey("UserId"));

            modelBuilder.Entity<Schedules>()
                .HasMany(e => e.PrivateRooms)
                .WithRequired(e => e.Schedules)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Schedules>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Schedules)
                .HasForeignKey(e => e.ChosenScheduleId);

            modelBuilder.Entity<Trimesters>()
                .Property(e => e.TrimesterName)
                .IsUnicode(false);

            modelBuilder.Entity<Trimesters>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Trimesters)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfiles>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserProfiles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.PrivateRooms)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(false);
        }
    }
}
