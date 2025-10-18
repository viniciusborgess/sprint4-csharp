using AutoMapper;
using Guardian.Api.Data;
using Guardian.Api.Domain;
using Guardian.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly GuardianDbContext _db; private readonly IMapper _mapper;
        public PlatformsController(GuardianDbContext db, IMapper mapper) { _db = db; _mapper = mapper; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> Get()
            => Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(await _db.Platforms.AsNoTracking().ToListAsync()));

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> Create(PlatformCreateDto dto)
        {
            var entity = _mapper.Map<BettingPlatform>(dto);
            _db.Platforms.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<PlatformReadDto>(entity));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Platforms.FindAsync(id);
            if (entity is null) return NotFound();
            _db.Platforms.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
