using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SampleBookingSystem.Common.CustomAttributes;
using SampleBookingSystem.Data.Common.Models;


namespace SampleBookingSystem.Data.Models
{
    public class Reservation : BaseModel<int>
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Users")]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Rooms")]
        public int RoomId { get; set; }

        [Required]
        [ValidCheckinDate]
        public DateTime CheckinDate { get; set; }

        [Required]
        public DateTime ChechoutDate { get; set; }

        public virtual Room Rooms { get; set; }

        public virtual User Users { get; set; }
    }
}
