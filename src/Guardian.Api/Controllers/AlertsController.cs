using AutoMapper;
using Guardian.Api.Data;
using Guardian.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly GuardianDbContext _db; private readonly IMapper _mapper;
        public AlertsController(GuardianDbContext db, IMapper mapper) { _db = db; _mapper = mapper; }

        [HttpGet("user/{userId:int}")]
public async Task<ActionResult<IEnumerable<AlertReadDto>>> GetByUser(int userId)
{
    var items = await _db.Alerts
        .AsNoTracking()
        .Where(a => a.UserId == userId)
        .ToListAsync(); // << sem ORDER BY no SQL

    var ordered = items
        .OrderByDescending(a => a.CreatedAt) // << ordena no cliente
        .ToList();

    return Ok(_mapper.Map<IEnumerable<AlertReadDto>>(ordered));
}

    }
}
