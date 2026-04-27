using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_14712.Models;
using System.Text.Json;

namespace WebApp_14712.Pages
{
    public class InfopaisModel : PageModel
    {
        // public void OnGet()
        // {
        // }
        private readonly IHttpClientFactory _httpClientFactory;
        public InfopaisModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Pais InfoPais { get; set; }
        public string CodigoPais { get; set; }

        public async Task<IActionResult> OnGetAsync(string cod)
        {
            CodigoPais = cod;
            var client = _httpClientFactory.CreateClient("RestCountries");
            var response = await client.GetAsync($"v3.1/alpha/{CodigoPais}?fields=name,cca2,flags");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var artigoResponse = JsonSerializer.Deserialize<CountryApiResponse>(json, options);
            
            InfoPais = new Pais
            {
                OfficialName = artigoResponse.name?.official,
                Cca2 = artigoResponse.cca2,
                FlagUrl = artigoResponse.flags?.png
            };

            return Page();
        }
    }
}