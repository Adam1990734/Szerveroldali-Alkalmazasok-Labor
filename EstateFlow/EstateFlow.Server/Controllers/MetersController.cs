using EstateFlow.Server.Data;
using EstateFlow.Server.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstateFlow.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetersController : ControllerBase
    {
        private readonly EstateFlowDbContext _context;

        public MetersController(EstateFlowDbContext context)
        {
            _context = context;
        }

        // GET: api/meters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meter>>> GetMeters()
        {
            var meters = await _context.Meters
                .Include(m => m.UtilityType)
                .Include(m => m.Provider)
                .ToListAsync();

            return Ok(meters);
        }

        // GET: api/meters/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Meter>> GetMeter(Guid id)
        {
            var meter = await _context.Meters
                .Include(m => m.UtilityType)
                .Include(m => m.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meter is null)
            {
                return NotFound();
            }

            return Ok(meter);
        }

        // GET: api/meters/{id}/readings
        [HttpGet("{id}/readings")]
        public async Task<ActionResult<IEnumerable<MeterReading>>> GetMeterReadings(Guid id)
        {
            var meterExists = await _context.Meters.AnyAsync(m => m.Id == id);

            if (!meterExists)
            {
                return NotFound();
            }

            var readings = await _context.MeterReadings
                .Where(r => r.MeterId == id)
                .OrderBy(r => r.MeasuredAt)
                .ToListAsync();

            return Ok(readings);
        }
    }
}
