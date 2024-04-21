using SolarSystemEncyclopedia.Models;

namespace SolarSystemEncyclopedia.ViewModels
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public Planet  Planet { get; set; }
        public Star Star { get; set; }
        public Moon Moon { get; set; }
    }
}
