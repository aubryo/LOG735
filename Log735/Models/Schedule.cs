namespace Log735
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScheduleId { get; set; }
    }
}
