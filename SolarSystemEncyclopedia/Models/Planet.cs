using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarSystemEncyclopedia.Models
{
    [Table("Planet")]

    public class Planet : CosmicObject
    {
        public bool HasAtmosphere { get; set; }

        public double? AtmosphericPressure { get; set; }

        public double? AverageTemperature { get; set; }

        public IEnumerable<Moon>? Moons { get; set; }

        public bool HasMoon { get; set; }

        [Column("MainStarId1")]
        public int MainStarId { get; set; }

        [ForeignKey("MainStarId")]
        public Star? MainStar { get; set; }

    }
}