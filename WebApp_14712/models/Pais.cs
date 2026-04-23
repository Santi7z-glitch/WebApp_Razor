namespace WebApp_14712.Models
{
    public class Pais
    {
        public string? OfficialName { get; set; }
        public string? Cca2 { get; set; }
        public string? FlagUrl { get; set; }
        public string? Capital { get; set; }
        public string? Region { get; set; }
        public string? Population { get; set; }
    }

    public class CountryApiResponse
    {
        public Name? name { get; set; }
        public string? cca2 { get; set; }
        public Flags? flags { get; set; }
        public string[]? capital { get; set; }
        public string? region { get; set; }
        public long? population { get; set; }
    }

    public class Name
    {
        public string? official { get; set; }
    }

    public class Flags
    {
        public string? png { get; set; }
    }

}