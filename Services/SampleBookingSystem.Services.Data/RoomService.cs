using System;
using System.Collections.Generic;
using System.Linq;
using SampleBookingSystem.Data.Common;
using SampleBookingSystem.Data.Models;
using SampleBookingSystem.Services.Data.Contracts;

namespace SampleBookingSystem.Services.Data
{
    public class RoomService : IRoomService
    {
        private IDbRepository<Room> rooms;

        public RoomService(IDbRepository<Room> rooms)
        {
            this.rooms = rooms;
        }

        public IQueryable<Room> GetAllFreeRooms()
        {
            var todayDate = DateTime.Now.Date;

            return this.rooms
                       .All()
                       .Where(r => r.Reservations.Count(re => 
                                                    re.RoomId == r.Id && 
                                                    re.CheckinDate == todayDate ||
                                                    (re.CheckinDate < todayDate && re.ChechoutDate > todayDate)) == 0);
        }

        public IQueryable<Room> GetRandomFreeRooms(int count)
        {
            return this.GetAllFreeRooms()
                       .OrderBy(r => Guid.NewGuid())
                       .Take(count);       
        }    
    }
}
