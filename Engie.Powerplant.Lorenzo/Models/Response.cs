using Newtonsoft.Json;

namespace Engie.Powerplant.Lorenzo.Models
{
    public class Response
    {
        public string Name { get; set; }

        public int P { get; set; }

        [JsonProperty("co2cost(€)")]
        public decimal CO2Cost { get; set; }

        [JsonProperty("cost(€)")]
        public decimal Cost { get; set; }
    }
}