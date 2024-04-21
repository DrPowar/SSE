using System.ComponentModel.DataAnnotations.Schema;

namespace SolarSystemEncyclopedia.Models
{
    [Table("Star")]

    public class Star : CosmicObject
    {
        public string SpectralClass { get; set; }

        public double Temperature { get; set; }

        public double Luminosity { get; set; }

        public bool HasPlanets { get; set; }

        public IEnumerable<Planet>? Planets { get; set; }
    }
}
