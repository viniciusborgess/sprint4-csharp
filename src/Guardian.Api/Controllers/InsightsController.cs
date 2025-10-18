using Guardian.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Guardian.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsightsController : ControllerBase
    {
        private readonly ExternalRatesClient _rates; private readonly InvestmentSimulator _sim;
        public InsightsController(ExternalRatesClient rates, InvestmentSimulator sim) { _rates = rates; _sim = sim; }

        [HttpGet("selic-last")] // dados externos
        public async Task<ActionResult<decimal?>> SelicLast() => Ok(await _rates.GetSelicDailyLastAsync());

        [HttpGet("simulate/{amount:decimal}/{months:int}")] // usa serviço local
        public async Task<ActionResult<object>> Simulate(decimal amount, int months)
        {
            var annual = await _rates.GetSelicDailyLastAsync() ?? 10m;
            var fv = _sim.LumpSumFutureValue(amount, annual, months);
            return Ok(new { amount, months, annualRate = annual, futureValue = fv });
        }
    }
}
