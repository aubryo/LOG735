namespace Log735
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserProfileId { get; set; }

        public int? DepartmentId { get; set; }

        public int? EnrollmentYear { get; set; }

        public int? CreditsToDate { get; set; }

        public virtual Department Department { get; set; }
    }
}
