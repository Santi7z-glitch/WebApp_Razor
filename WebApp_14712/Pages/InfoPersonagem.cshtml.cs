using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_14712.Models;
using System.Text.Json;

namespace WebApp_14712.Pages
{
    public class InfoPersonagemModel : PageModel
    {
        // public void OnGet()
        // {
        // }
        private readonly IHttpClientFactory _httpClientFactory;
        public InfoPersonagemModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public DragonBall InfoPersonagem { get; set; }
        public string CodigoPersonagem { get; set; }

        public async Task<IActionResult> OnGetAsync(string cod)
        {
            CodigoPersonagem = cod;
            var client = _httpClientFactory.CreateClient("DragonBall");
            var response = await client.GetAsync($"characters/{CodigoPersonagem}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var personagemResponse = JsonSerializer.Deserialize<DragonBallApiResponse>(json, options);
            
            InfoPersonagem = new DragonBall
            {
                Name = personagemResponse.name,
                ImgUrl = personagemResponse.imgUrl.png
            };

            return Page();
        }
    }
}