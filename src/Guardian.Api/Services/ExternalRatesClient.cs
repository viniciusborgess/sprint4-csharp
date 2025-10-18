using System.Net.Http.Json;
using Polly;
using Polly.Extensions.Http;

namespace Guardian.Api.Services
{
    public class ExternalRatesClient
    {
        private readonly HttpClient _http;
        public ExternalRatesClient(HttpClient http) => _http = http;

        public class BcbSerieItem { public string? data { get; set; } public string? valor { get; set; } }

        // Exemplo: SELIC diária (série 432) – pega último valor.
        public async Task<decimal?> GetSelicDailyLastAsync(CancellationToken ct = default)
{
    try
    {
        var url = "https://api.bcb.gov.br/dados/serie/bcdata.sgs.432/dados/ultimo?formato=json";
        using var resp = await _http.GetAsync(url, ct);

        if (!resp.IsSuccessStatusCode)
            return null; // << não explode; deixamos o caller decidir o fallback

        var data = await resp.Content.ReadFromJsonAsync<List<BcbSerieItem>>(cancellationToken: ct);
        if (data?.Count > 0 &&
            decimal.TryParse(
                data[0].valor?.Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var v))
        {
            return v; // % a.a. (aprox.)
        }
        return null;
    }
    catch
    {
        return null; // << qualquer erro de rede/parse vira null
    }
}

    }
}
