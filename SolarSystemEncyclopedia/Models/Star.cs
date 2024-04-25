using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SolarSystemEncyclopedia.Models
{
    [Table("Star")]

    public class Star : CosmicObject
    {
        public string SpectralClass { get; set; }

        public double Temperature { get; set; }

        public double Luminosity { get; set; }

        public bool HasPlanets { get; set; }

        [JsonIgnore]
        public IEnumerable<Planet>? Planets { get; set; }
    }
}
