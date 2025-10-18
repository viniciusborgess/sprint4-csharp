using AutoMapper;
using Guardian.Api.Data;
using Guardian.Api.Domain;
using Guardian.Api.DTOs;
using Guardian.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransfersController : ControllerBase
    {
        private readonly GuardianDbContext _db;
        private readonly IMapper _mapper;
        private readonly AdvisorService _advisor;
        private readonly ExternalRatesClient _rates;

        public TransfersController(
            GuardianDbContext db,
            IMapper mapper,
            AdvisorService advisor,
            ExternalRatesClient rates)
        {
            _db = db; _mapper = mapper; _advisor = advisor; _rates = rates;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferReadDto>>> Get()
            => Ok(_mapper.Map<IEnumerable<TransferReadDto>>(
                await _db.Transfers.AsNoTracking().ToListAsync()));

        // LINQ: relatório por usuário/plataforma nos últimos 30 dias
        [HttpGet("report/last30/{userId:int}")]
        public async Task<ActionResult> ReportLast30(int userId)
        {
            var last30 = DateTimeOffset.UtcNow.AddDays(-30);
            var query = _db.Transfers.AsNoTracking()
                .Where(t => t.UserId == userId && t.CreatedAt >= last30 && t.Status != TransferStatus.Cancelled)
                .GroupBy(t => t.PlatformId)
                .Select(g => new
                {
                    PlatformId = g.Key,
                    Total = g.Sum(x => x.Amount),
                    Count = g.Count(),
                    Avg = g.Average(x => x.Amount)
                })
                .OrderByDescending(x => x.Total);

            var result = await query.ToListAsync();
            return Ok(result);
        }

        // Cria transferência + gera alerta persuasivo baseado em taxa externa (SELIC) com fallback
        [HttpPost]
        public async Task<ActionResult<TransferReadDto>> Create(TransferCreateDto dto)
        {
            var entity = _mapper.Map<PixTransfer>(dto);
            _db.Transfers.Add(entity);
            await _db.SaveChangesAsync();

            // taxa anual aproximada com fallback resiliente
            decimal annual;
            try
            {
                annual = await _rates.GetSelicDailyLastAsync() ?? 10m; // fallback 10% a.a.
            }
            catch
            {
                annual = 10m; // se a chamada externa lançar exceção, usa fallback
            }

            var msg = await _advisor.BuildPreTransferMessage(entity.UserId, entity.PlatformId, entity.Amount, annual);
            _db.Alerts.Add(new Alert { UserId = entity.UserId, TransferId = entity.Id, Message = msg });
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<TransferReadDto>(entity));
        }

        // Endpoint de "segunda vez": mensagem recorrente personalizada com fallback
        [HttpGet("advice/again/{userId:int}/{platformId:int}")]
        public async Task<ActionResult<string>> AdviceAgain(int userId, int platformId)
        {
            decimal annual;
            try
            {
                annual = await _rates.GetSelicDailyLastAsync() ?? 10m;
            }
            catch
            {
                annual = 10m;
            }

            var msg = await _advisor.BuildRecurrentMessage(userId, platformId, annual);
            return Ok(msg);
        }
    }
}
