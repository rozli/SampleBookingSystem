using SampleBookingSystem.Data.Models;

namespace SampleBookingSystem.Services.Data.Contracts
{
    public interface IReservationsService
    {
        int AddReservation(Reservation newReservation);
    }
}
