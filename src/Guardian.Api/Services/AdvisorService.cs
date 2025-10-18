using Guardian.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Api.Services
{
    public class AdvisorService
    {
        private readonly GuardianDbContext _db;
        private readonly InvestmentSimulator _simulator;
        public AdvisorService(GuardianDbContext db, InvestmentSimulator simulator)
        {
            _db = db; _simulator = simulator;
        }

        public async Task<string> BuildPreTransferMessage(int userId, int platformId, decimal amount, decimal annualRate)
        {
            var user = await _db.Users.FindAsync(userId);
            var platform = await _db.Platforms.FindAsync(platformId);
            var fv12 = _simulator.LumpSumFutureValue(amount, annualRate, 12);
            return $"Você está prestes a transferir R${amount:F2} para {platform?.Name}. Se investir este valor a {annualRate:F2}% a.a., em 12 meses poderia ter ~R${fv12:F2}.";
        }

        public async Task<string> BuildRecurrentMessage(int userId, int platformId, decimal annualRate)
        {
            var last30 = DateTimeOffset.UtcNow.AddDays(-30);
            var total = await _db.Transfers
                .Where(t => t.UserId == userId && t.PlatformId == platformId && t.Status != Domain.TransferStatus.Cancelled && t.CreatedAt >= last30)
                .SumAsync(t => (decimal?)t.Amount) ?? 0m;
            var fv12 = _simulator.FutureValue(total, annualRate, 12);
            var platform = await _db.Platforms.FindAsync(platformId);
            return $"Nos últimos 30 dias, você transferiu R${total:F2} para {platform?.Name}. Se investido a {annualRate:F2}% a.a. (depósitos mensais), em 12 meses poderia ter ~R${fv12:F2}.";
        }
    }
}
