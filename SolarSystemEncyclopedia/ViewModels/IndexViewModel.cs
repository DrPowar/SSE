using SolarSystemEncyclopedia.Models;

namespace SolarSystemEncyclopedia.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(IEnumerable<Moon> moons, IEnumerable<Planet> planets, IEnumerable<Star> stars)
        {
            Planets = planets;
            Stars = stars;
            Moons = moons;
        }
        public IndexViewModel()
        {

        }

        public IEnumerable<Planet> Planets { get; set; }
        public IEnumerable<Star> Stars { get; set; }
        public IEnumerable<Moon> Moons { get; set; }

        public bool AnyPlanets()
        {
            return (Planets.Any());
        }
        public bool AnyStars()
        {
            return (Stars.Any());
        }
        public bool AnyMoons()
        {
            return (Moons.Any());
        }

    }
}
