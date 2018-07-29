namespace PrivateRoomDomain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PrivateRooms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PrivateRooms()
        {
            Users1 = new HashSet<Users>();
        }

        [Key]
        public int RoomId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomName { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomPassword { get; set; }

        public int OwnerId { get; set; }

        public int ScheduleId { get; set; }

        public virtual ActiveRooms ActiveRooms { get; set; }

        public virtual Users Users { get; set; }

        public virtual Schedules Schedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users1 { get; set; }
    }
}
