namespace PrivateRoomDomain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseInfo")]
    public partial class CourseInfo
    {
        public int CourseInfoId { get; set; }

        public int CourseId { get; set; }

        [Required]
        [StringLength(10)]
        public string StartTime { get; set; }

        [Required]
        [StringLength(10)]
        public string EndTime { get; set; }

        [Required]
        [StringLength(20)]
        public string WDay { get; set; }

        public bool BIsLab { get; set; }

        public virtual Courses Courses { get; set; }
    }
}
