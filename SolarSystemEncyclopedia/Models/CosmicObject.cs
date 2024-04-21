using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SolarSystemEncyclopedia.Models
{
    [Table("CosmicObject")]
    public class CosmicObject
    {

        public CosmicObject()
        {
            if (!string.IsNullOrEmpty(StringDensity) && double.TryParse(StringDensity, out double result))
            {
                Density = result;
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //A string type field is used here, as the values are very large, and we will record them using exponential notation. Like this: 6,4171⋅10^23
        public string Mass { get; set; }

        //A string type field is used here, as the values are very large, and we will record them using exponential notation. Like this: 6,4171⋅10^23
        public string Volume { get; set; }

        //A string type field is used here, as the values are very large, and we will record them using exponential notation. Like this: 6,4171⋅10^23
        public string SurfaceArea { get; set; }

        public long Radius { get; set; }

        public double? Density { get; set; }

        [NotMapped]
        public string? StringDensity { get; set; }

        [NotMapped]
        public IFormFile? MainImage { get; set; }
    }
}
