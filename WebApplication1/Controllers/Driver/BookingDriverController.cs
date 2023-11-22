using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Data.EF;
using WebApplication1.Data.Model;
using WebApplication1.Function;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDriverController : ControllerBase
    {
        private readonly MyDbContext _context;
        public BookingDriverController(MyDbContext context)
        {
            _context = context;
        }
        // GET: api/Trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            return await _context.Trips.ToListAsync();
        }
        // POST: api/Trips/AcceptTrip
        [HttpPost("AcceptTrip")]
        public async Task<IActionResult> AcceptTrip(BookingDriver model)
        {
            var trip = await _context.Trips.FindAsync(model.tripId);
            if (trip == null)
            {
                return NotFound();
            }
            else
            {
                trip.status = "da nhan don";
                trip.DriverId = model.driverId;
             
                await _context.SaveChangesAsync();
                return Ok(new { success = true });
            }        
        }
        [HttpPost("RunningTrip")]
        public async Task<IActionResult> RunningTrip(BookingDriver model)
        {
            var trip = await _context.Trips.FindAsync(model.tripId);
            if (trip == null)
            {
                return NotFound();
            }
            else
            {
                trip.status = "dang chay";
                await _context.SaveChangesAsync();
                return Ok(new { success = true });
            }
        }

        [HttpPost("DoneTrip")]
        public async Task<IActionResult> DoneTrip(BookingDriver model)
        {
            var trip = await _context.Trips.FindAsync(model.tripId);
            if (trip == null)
            {
                return NotFound();
            }
            else
            {
                trip.isPaid = true;
                trip.status = "hoan thanh";
                await _context.SaveChangesAsync();
                return Ok(new { success = true });
            }
        }

        [HttpPost("ErrorTrip")]
        public async Task<IActionResult> ErrorTrip(BookingDriver model)
        {
            var trip = await _context.Trips.FindAsync(model.tripId);
            if (trip == null)
            {
                return NotFound();
            }
            else
            {
                trip.isPaid = true;
                trip.status = "da huy";
                await _context.SaveChangesAsync();
                return Ok(new { success = true });
            }
        }

    }
}
