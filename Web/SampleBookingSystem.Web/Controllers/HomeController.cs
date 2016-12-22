using System.Linq;
using System.Web.Mvc;
using SampleBookingSystem.Common.Mappings;
using SampleBookingSystem.Services.Data.Contracts;
using SampleBookingSystem.Web.Models.Home;

namespace SampleBookingSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IRoomService rooms;

        public HomeController(IRoomService roomsService)
        {
            this.rooms = roomsService;
        }

        public ActionResult Index()
        {
            var rooms = this.rooms
                            .GetRandomFreeRooms(5)
                            .To<RoomViewModel>()
                            .ToList();

            return View(rooms);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string News(int page = 1)
        {
            return page.ToString();
        }
    }
}