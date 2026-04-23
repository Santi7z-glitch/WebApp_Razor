using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_14712.Models;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApp_14712.Pages;

public class InfopaisModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InfopaisModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public string? CodigoPais { get; set; }
    public Pais? InfoPais { get; set; }

    public async Task OnGetAsync(string? cod)
    {
        // Guardar o código do país recebido
        CodigoPais = string.IsNullOrWhiteSpace(cod) ? "PT" : cod.ToUpperInvariant();

        // Chamar a API para obter os dados do país
        var client = _httpClientFactory.CreateClient("RestCountries");
        var response = await client.GetAsync($"v3.1/alpha/{CodigoPais}");

        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        // Tratar o JSON retornado pela API
        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var dados = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options);

        if (dados?.Count > 0)
        {
            var primeiro = dados[0];
            
            // Colocar a informação que precisamos no objeto InfoPais
            InfoPais = new Pais
            {
                OfficialName = primeiro.name?.official,
                Cca2 = primeiro.cca2,
                FlagUrl = primeiro.flags?.png,
                Capital = primeiro.capital?.FirstOrDefault() ?? "N/A",
                Region = primeiro.region,
                Population = primeiro.population?.ToString("N0") ?? "N/A"
            };
        }
    }
}

