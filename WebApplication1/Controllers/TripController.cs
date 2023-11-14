
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.EF;
using WebApplication1.Data.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly MyDbContext _context;
        public TripController(MyDbContext context)
        {
            _context = context;
        }
        [HttpPost("Booking")]
        public IActionResult BookingTrip(BookingViewModel model)
        {
            if (model != null) 
            {
                var trip = new Trip
                {
                    UserId = model.UserId,
                    distance = model.distance,
                    time = model.time,
                    timeBook = DateTime.Now,
                    startLocation = model.startLocation,
                    endLocation = model.endLocation,
                    price = model.price,
                    status = "Chưa nhận",
                    isPaid = false,
                };
                _context.Trips.Add(trip);
                _context.SaveChanges();
                return Ok("Booking successful");
            }
            return BadRequest("Booking failed");
        }
    }
}
