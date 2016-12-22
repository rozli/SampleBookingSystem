using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SampleBookingSystem.Data.Models;
using SampleBookingSystem.Services.Data.Contracts;
using SampleBookingSystem.Web.Models.Reservation;

namespace SampleBookingSystem.Web.Controllers
{
    [Authorize]
    public class ReservationController : BaseController
    {
        private IReservationsService reservations;
        private IRoomService rooms;

        public ReservationController(IReservationsService reservations, IRoomService rooms)
        {
            this.reservations = reservations;
            this.rooms = rooms;
        }

        [HttpGet]
        public ActionResult BookRooms()
        {
            var freeRooms = this.rooms.GetAllFreeRooms()
                                      .Select(r => r.Id)
                                      .ToList();

            var model = new CreateReservationViewModel()
            {
                FreeRoomsList = new SelectList(freeRooms, "RoomId")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookRooms(CreateReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newReservation = new Reservation
                {
                    UserId = User.Identity.GetUserId(),
                    RoomId = model.RoomId,
                    CheckinDate = model.CheckinDate,
                    ChechoutDate = model.CheckoutDate
                };

                int newReservationId = reservations.AddReservation(newReservation);

                return Content($"Reservation added: {newReservationId}");
            }

            var freeRooms = this.rooms.GetAllFreeRooms().Select(r => r.Id).ToList();
            model.FreeRoomsList = new SelectList(freeRooms, "RoomId");

            return View(model);
        }
    }
}