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
    public class UsersController : ControllerBase
    {
        private readonly GuardianDbContext _db; private readonly IMapper _mapper;
        public UsersController(GuardianDbContext db, IMapper mapper) { _db = db; _mapper = mapper; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> Get()
            => Ok(_mapper.Map<IEnumerable<UserReadDto>>(await _db.Users.AsNoTracking().ToListAsync()));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserReadDto>> GetById(int id)
        {
            var entity = await _db.Users.FindAsync(id);
            return entity is null ? NotFound() : Ok(_mapper.Map<UserReadDto>(entity));
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> Create(UserCreateDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            _db.Users.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<UserReadDto>(entity));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UserCreateDto dto)
        {
            var entity = await _db.Users.FindAsync(id);
            if (entity is null) return NotFound();
            entity.Name = dto.Name; entity.Email = dto.Email;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Users.FindAsync(id);
            if (entity is null) return NotFound();
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
