using System.Linq;
using SampleBookingSystem.Data.Models;

namespace SampleBookingSystem.Services.Data.Contracts
{
    public interface IRoomService
    {
        IQueryable<Room> GetRandomFreeRooms(int count);

        IQueryable<Room> GetAllFreeRooms();
    }
}
