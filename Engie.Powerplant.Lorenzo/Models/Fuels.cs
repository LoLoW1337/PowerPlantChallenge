using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Engie.Powerplant.Lorenzo.Models
{
    public class Fuels
    {
        [JsonProperty("gas(euro/MWh)")]
        public decimal Gas { get; set; }

        [JsonProperty("kerosine(euro/MWh)")]
        public decimal Kerosine { get; set; }

        [JsonProperty("co2(euro/ton)")]
        public decimal Co2 { get; set; }

        [JsonProperty("wind(%)")]
        public int Wind { get; set; }
    }
}
