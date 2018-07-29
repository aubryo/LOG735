namespace PrivateRoomDomain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            PrivateRooms = new HashSet<PrivateRooms>();
            PrivateRooms1 = new HashSet<PrivateRooms>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(15)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserPassword { get; set; }

        public int UserProfileId { get; set; }

        public int? ChosenScheduleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrivateRooms> PrivateRooms { get; set; }

        public virtual Schedules Schedules { get; set; }

        public virtual UserProfiles UserProfiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrivateRooms> PrivateRooms1 { get; set; }
    }
}
