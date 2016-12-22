using System;
using System.Collections.Generic;
using System.Linq;
using SampleBookingSystem.Data.Common;
using SampleBookingSystem.Data.Models;
using SampleBookingSystem.Services.Data.Contracts;

namespace SampleBookingSystem.Services.Data
{
    public class ReservationService : IReservationsService
    {
        private IDbRepository<Reservation> reservations;

        public ReservationService(IDbRepository<Reservation> reservations)
        {
            this.reservations = reservations;
        }

        public int AddReservation(Reservation newReservation)
        {
            this.reservations.Add(newReservation);
            this.reservations.Save();

            return newReservation.Id;
        }
    }
}
