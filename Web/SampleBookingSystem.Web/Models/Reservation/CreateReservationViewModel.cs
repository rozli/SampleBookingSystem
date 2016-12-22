using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SampleBookingSystem.Common;
using SampleBookingSystem.Common.CustomAttributes;

namespace SampleBookingSystem.Web.Models.Reservation
{
    public class CreateReservationViewModel
    {
        [Required(ErrorMessage = GlobalConstants.MissingRoomIdMessage)]
        [Range(1, 16)]
        public int RoomId { get; set; }

        [Required]
        [ValidCheckinDate(ErrorMessage = GlobalConstants.CheckinDateErrorMessage)]
        [DataType(DataType.Date)]
        public DateTime CheckinDate { get; set; } = DateTime.Now;

        [Required]
        [ValidCheckoutDate(ErrorMessage = GlobalConstants.CheckoutDateErrorMessage)]
        [DataType(DataType.Date)]
        public DateTime CheckoutDate { get; set; } = DateTime.Now;

        public SelectList FreeRoomsList { get; set; }
    }
}