namespace Log735
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Cours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [StringLength(10)]
        public string CourseAcronym { get; set; }

        [StringLength(50)]
        public string CourseName { get; set; }

        public int? SubscribedCount { get; set; }

        public int? MaxSubscription { get; set; }

        [StringLength(50)]
        public string ScheduledTime { get; set; }

        public int? TrimesterId { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Trimester Trimester { get; set; }
    }
}
