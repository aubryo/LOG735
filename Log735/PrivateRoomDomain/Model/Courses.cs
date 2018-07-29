namespace PrivateRoomDomain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Courses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Courses()
        {
            CourseInfo = new HashSet<CourseInfo>();
            Schedules = new HashSet<Schedules>();
        }

        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(10)]
        public string CourseAcronym { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseName { get; set; }

        public int SubscribedCount { get; set; }

        public int MaxSubscription { get; set; }

        public int TrimesterId { get; set; }

        public int DepartmentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseInfo> CourseInfo { get; set; }

        public virtual Departments Departments { get; set; }

        public virtual Trimesters Trimesters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedules> Schedules { get; set; }
    }
}
