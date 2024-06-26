﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SolarSystemEncyclopedia.Models
{
    [Table("Moon")]

    public class Moon : CosmicObject 
    {
        public bool HasAtmosphere { get; set; }

        public double? AtmosphericPressure { get; set; }

        public double? AverageTemperature { get; set; }

        [Column("MainPlanetId")]
        public int MainPlanetId { get; set; }

        [JsonIgnore]
        [ForeignKey("MainPlanetId")]
        public Planet? MainPlanet { get; set; }
    }
}
