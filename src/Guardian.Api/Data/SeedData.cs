using Guardian.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Api.Data
{
    public static class SeedData
    {
        public static async Task EnsureSeededAsync(this GuardianDbContext db)
        {
            await db.Database.MigrateAsync();

            if (!await db.Platforms.AnyAsync())
            {
                db.Platforms.AddRange(new []
                {
                    new BettingPlatform { Name = "ApostaX", PixKey = "aposta@pix.com", Website = "https://aposta-x.example" },
                    new BettingPlatform { Name = "BetY", PixKey = "+55 11 99999-9999", Website = "https://bet-y.example" }
                });
            }

            if (!await db.Users.AnyAsync())
            {
                db.Users.Add(new User { Name = "Vinicius", Email = "vinicius@example.com" });
            }

            await db.SaveChangesAsync();
        }
    }
}
