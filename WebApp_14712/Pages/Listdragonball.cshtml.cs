using Microsoft.AspNetCore.Mvc.RazorPages;

using WebApp_14712.Models;

using System.Text.Json;

namespace WebApp_14712.Pages;

public class DragonBallModel : PageModel

{

    private readonly IHttpClientFactory _httpClientFactory;

    public DragonBallModel(IHttpClientFactory httpClientFactory)

    {

        _httpClientFactory = httpClientFactory;

    }

    public List<DragonBall> DragonBalls { get; set; }

    public async Task OnGetAsync()

    {

        var client = _httpClientFactory.CreateClient("DragonBall");

        var response = await client.GetAsync("/characters");

        if (response.IsSuccessStatusCode)

        {

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var dados = JsonSerializer.Deserialize<List<DragonBallApiResponse>>(json, options);


            DragonBalls = dados.Select(d => new DragonBall

            {

                Name = d.name?.ToString(),

                ImgUrl = d.imgUrl?.png

            }).ToList();

        }

    }

}