using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SampleBookingSystem.Data.Common.Models;

namespace SampleBookingSystem.Data.Models
{
    public class Room  : BaseModel<int>
    {
        [Required(ErrorMessage = "Room number is required!")]
        [Range(1, 15)]
        public int RoomNumber { get; set; }

        [Required]
        [Range(1, 6)]
        public int Capacity { get; set; }

        [Range(1, 3)]
        public int Floor { get; set; }

        [StringLength(200, ErrorMessage = "Room description cannot be longer than 200 symbols")]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
