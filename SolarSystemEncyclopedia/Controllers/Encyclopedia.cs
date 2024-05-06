using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SolarSystemEncyclopedia.Data;
using SolarSystemEncyclopedia.Models;
using SolarSystemEncyclopedia.ViewModels;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using System;
using SolarSystemEncyclopedia.Algorithms;
using System.Reflection;


namespace SolarSystemEncyclopedia.Controllers
{
    public class Encyclopedia : Controller
    {
        private readonly SolarSystemContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public Encyclopedia(SolarSystemContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var starsWithPlanets = _context.Star.Include(s => s.Planets).ToList();
            var moons = await _context.Moon.ToListAsync();
            var planets = starsWithPlanets.SelectMany(s => s.Planets).ToList();


            if (!starsWithPlanets.Any() && !moons.Any() && !planets.Any())
            {
                return Problem("Something wrong with database or context!!!");
            }


            IndexViewModel ivm = new IndexViewModel(moons, planets, starsWithPlanets);

            return View(ivm);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            List<Planet> planets = new List<Planet>();
            List<Star> stars = new List<Star>();
            List<Moon> moons = new List<Moon>();



            if (searchTerm != null)
            {
                string[] splitSearchTerm = SearchAlgorithms.SplitSearchString(searchTerm);
                IndexViewModel ivm2 = ComplexSearch(splitSearchTerm);
                return PartialView("_IndexPartial", ivm2);
            }
            else
            {
                stars = await _context.Star.Include(p => p.Planets).ToListAsync();
                moons = await _context.Moon.ToListAsync();
                planets = stars.SelectMany(s => s.Planets).ToList();

                IndexViewModel ivm = new IndexViewModel(moons, planets, stars);

                return PartialView("_IndexPartial", ivm);
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(string json)
        {
            var jsondata = JsonConvert.DeserializeObject<dynamic>(json);
            string objectType = Convert.ToString(jsondata["objectType"]);
            switch (objectType)
            {
                case "Star":
                    return PartialView("_CreateStarPartial");
                case "Planet":
                    var stars = _context.Star.ToList();
                    ViewBag.Stars = stars;
                    return PartialView("_CreatePlanetPartial");
                case "Moon":
                    var planets = _context.Planet.ToList();
                    ViewBag.Planets = planets;
                    return PartialView("_CreateMoonPartial");
                default:
                    return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlanet(Planet planet)
        {
            if (ModelState.IsValid == true)
            {
                var star = _context.Star.FirstOrDefault(s => s.Id == planet.MainStarId);
                planet.MainStar = star;
                if (star.HasPlanets == false)
                {
                    star.HasPlanets = true;
                    _context.Update(star);
                }
                planet.Density = Double.Parse(planet.StringDensity);
                await ImageCreate(planet);
                _context.Add(planet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("_CreatePlanetPartial", planet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStar(Star star)
        {
            if (ModelState.IsValid == true)
            {
                star.Density = Double.Parse(star.StringDensity);
                await ImageCreate(star);
                _context.Add(star);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("_CreateStarPartial", star);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoon(Moon moon)
        {
            if (ModelState.IsValid == true)
            {
                var planet = _context.Planet.FirstOrDefault(s => s.Id == moon.MainPlanetId);
                moon.MainPlanet = planet;
                if (planet.HasMoon == false)
                {
                    planet.HasMoon = true;
                    _context.Update(planet);
                }
                moon.Density = Double.Parse(moon.StringDensity);
                await ImageCreate(moon);
                _context.Add(moon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("_CreateMoonPartial", moon);
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planet = _context.Planet.Find(id);
            var star = _context.Star.Find(id);
            var moon = _context.Moon.Find(id);
            if (planet != null)
            {
                planet.StringDensity = planet.Density.ToString();
                return View("_UpdatePlanet", planet);
            }
            else if (star != null)
            {
                star.StringDensity = star.Density.ToString();
                return View("_UpdateStar", star);
            }
            else if (moon != null)
            {
                moon.StringDensity = moon.Density.ToString();
                return View("_UpdateMoon", moon);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlanet(Planet planet, int id)
        {
            if (ModelState.IsValid == true)
            {
                var star = _context.Star.FirstOrDefault(s => s.Id == planet.MainStarId);
                planet.MainStar = star;
                if (planet.StringDensity != null)
                {
                    planet.Density = Double.Parse(planet.StringDensity);
                }
                await ImageCreate(planet);
                _context.Update(planet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("_UpdatePlanet", planet);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStar(Star star, int id)
        {
            if (ModelState.IsValid == true)
            {
                if (star.StringDensity != null)
                {
                    star.Density = Double.Parse(star.StringDensity);
                }
                await ImageCreate(star);
                _context.Update(star);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("_UpdateStar", star);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMoon(Moon moon, int id)
        {
            if (ModelState.IsValid == true)
            {
                var planet = _context.Planet.FirstOrDefault(s => s.Id == moon.MainPlanetId);
                moon.MainPlanet = planet;
                if (moon.StringDensity != null)
                {
                    moon.Density = Double.Parse(moon.StringDensity);
                }
                await ImageCreate(moon);
                _context.Update(moon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("_UpdateMoon", moon);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var planet = await _context.Planet.FindAsync(id);
            var star = await _context.Star.FindAsync(id);
            var moon = await _context.Moon.FindAsync(id);
            DeleteViewModel dvm = new DeleteViewModel();

            if (planet != null)
            {
                dvm.Planet = planet;
                return View(dvm);
            }
            else if (star != null)
            {
                dvm.Star = star;
                return View(dvm);
            }
            else if (moon != null)
            {
                dvm.Moon = moon;
                return View(dvm);
            }
            else
            {
                return NotFound();
            }
        }



        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planet = await _context.Planet.FindAsync(id);
            var star = await _context.Star.FindAsync(id);
            var moon = await _context.Moon.FindAsync(id);

            if (planet != null)
            {
                ImageDelete(planet);
                _context.Planet.Remove(planet);
                await _context.SaveChangesAsync();
            }
            else if (star != null)
            {
                ImageDelete(star);
                _context.Star.Remove(star);
                await _context.SaveChangesAsync();
            }
            else if (moon != null)
            {
                ImageDelete(moon);
                _context.Moon.Remove(moon);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task ImageCreate<T>(T obj) where T : CosmicObject
        {
            if (obj.MainImage != null)
            {
                string uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "Graphics", "ObjectImages", obj.Name);
                string uniqueFileName = obj.Name + "MainImage.png";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await obj.MainImage.CopyToAsync(fileStream);
                }
            }
        }

        public async Task ImageDelete<T>(T obj) where T : CosmicObject
        {
            if (obj.MainImage != null)
            {
                string directoryPath = Path.Combine(_appEnvironment.WebRootPath, "Graphics", "ObjectImages", obj.Name);

                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath);
                }
            }
        }

        public IndexViewModel ComplexSearch(string[] splitSearchTerm)
        {
            IndexViewModel indexViewModel = new IndexViewModel();

            foreach (var searchTerm in splitSearchTerm)
            {
                indexViewModel = FilterObjects(searchTerm, indexViewModel).Result;
            }

            return indexViewModel;
        }

        public async Task<IndexViewModel> FilterObjects(string filter, IndexViewModel collection)
        {

            var stars = _context.Star.Include(s => s.Planets).ToList();
            var moons = await _context.Moon.ToListAsync();
            var planets = stars.SelectMany(s => s.Planets).ToList();


            if (collection.Stars != null || collection.Planets != null || collection.Moons != null) //Uses if we already have some collection from searchTerm
            {
                string modelTypeFilter = SearchAlgorithms.ModelTypeFilter(filter);

                if(modelTypeFilter != "")
                {
                    return CollectionModelBasedSearch(modelTypeFilter, collection);
                }

                var (field, operatorSymbol, value) = SearchAlgorithms.ParseFilter(filter);

                var (targetValue, isExponentTargetValue) = SearchAlgorithms.ParseScientificNotation(value);

                stars = collection.Stars
                .Where(s =>
                {
                    PropertyInfo propertyInfo = s.GetType().GetProperty(field);

                    if (propertyInfo == null)
                    {
                        return false;
                    }

                    var (parsedField, isExponentParsedField) = SearchAlgorithms.ParseScientificNotation(s.GetType().GetProperty(field).GetValue(s).ToString());
                    return SearchAlgorithms.Compare(parsedField, operatorSymbol, targetValue, isExponentTargetValue, isExponentParsedField);
                })
                .ToList();

                planets = collection.Planets
                .Where(s =>
                {
                    PropertyInfo propertyInfo = s.GetType().GetProperty(field);

                    if (propertyInfo == null)
                    {
                        return false;
                    }

                    var (parsedField, isExponentParsedField) = SearchAlgorithms.ParseScientificNotation(s.GetType().GetProperty(field).GetValue(s).ToString());
                    return SearchAlgorithms.Compare(parsedField, operatorSymbol, targetValue, isExponentTargetValue, isExponentParsedField);
                })

                .ToList();

                moons = collection.Moons
                .Where(s =>
                {
                    PropertyInfo propertyInfo = s.GetType().GetProperty(field);

                    if (propertyInfo == null)
                    {
                        return false;
                    }

                    var (parsedField, isExponentParsedField) = SearchAlgorithms.ParseScientificNotation(s.GetType().GetProperty(field).GetValue(s).ToString());
                    return SearchAlgorithms.Compare(parsedField, operatorSymbol, targetValue, isExponentTargetValue, isExponentParsedField);
                })
                .ToList();

                IndexViewModel viewModel = new IndexViewModel(moons, planets, stars);

                return viewModel;
            }
            else //Uses if we process collection with first searchTerm
            {

                string modelTypeFilter = SearchAlgorithms.ModelTypeFilter(filter);

                if (modelTypeFilter != "")
                {
                    return DBModelBasedSearch(modelTypeFilter);
                }

                var (field, operatorSymbol, value) = SearchAlgorithms.ParseFilter(filter);

                var (targetValue, isExponentTargetValue) = SearchAlgorithms.ParseScientificNotation(value);

                stars = stars
                .Where(s =>
                {
                    PropertyInfo propertyInfo = s.GetType().GetProperty(field);

                    if (propertyInfo == null)
                    {
                        return false;
                    }

                    var (parsedField, isExponentParsedField) = SearchAlgorithms.ParseScientificNotation(s.GetType().GetProperty(field).GetValue(s).ToString());
                    return SearchAlgorithms.Compare(parsedField, operatorSymbol, targetValue, isExponentTargetValue, isExponentParsedField);
                })
                .ToList();

                planets = planets
                .Where(s =>
                {
                    PropertyInfo propertyInfo = s.GetType().GetProperty(field);

                    if (propertyInfo == null)
                    {
                        return false;
                    }

                    var (parsedField, isExponentParsedField) = SearchAlgorithms.ParseScientificNotation(s.GetType().GetProperty(field).GetValue(s).ToString());
                    return SearchAlgorithms.Compare(parsedField, operatorSymbol, targetValue, isExponentTargetValue, isExponentParsedField);
                })

                .ToList();

                moons = moons
                .Where(s =>
                {
                    PropertyInfo propertyInfo = s.GetType().GetProperty(field);

                    if (propertyInfo == null)
                    {
                        return false;
                    }

                    var (parsedField, isExponentParsedField) = SearchAlgorithms.ParseScientificNotation(s.GetType().GetProperty(field).GetValue(s).ToString());
                    return SearchAlgorithms.Compare(parsedField, operatorSymbol, targetValue, isExponentTargetValue, isExponentParsedField);
                })
                .ToList();

                IndexViewModel viewModel = new IndexViewModel(moons, planets, stars);

                return viewModel;
            }

        }

        public IndexViewModel CollectionModelBasedSearch(string filter, IndexViewModel collection)
        {
            if (filter == "Star")
            {
                var starsModelFilter = collection.Stars.ToList();
                IndexViewModel modelBaseVM = new IndexViewModel(new List<Moon>(), new List<Planet>(), starsModelFilter);
                return modelBaseVM;
            }
            else if (filter == "Planet")
            {
                var planetModelFilter = collection.Planets.ToList();
                IndexViewModel modelBaseVM = new IndexViewModel(new List<Moon>(), planetModelFilter, new List<Star>());
                return modelBaseVM;
            }
            else if (filter == "Moon")
            {
                var moonModelFilter = collection.Moons.ToList();
                IndexViewModel modelBaseVM = new IndexViewModel(moonModelFilter, new List<Planet>(), new List<Star>());
                return modelBaseVM;
            }
            else
            {
                return null;
            }
        }

        public IndexViewModel DBModelBasedSearch(string filter)
        {
            if (filter == "Star")
            {
                var starsModelFilter = _context.Star.ToList();
                IndexViewModel modelBaseVM = new IndexViewModel(new List<Moon>(), new List<Planet>(), starsModelFilter);
                return modelBaseVM;
            }
            else if (filter == "Planet")
            {
                var planetModelFilter = _context.Planet.ToList();
                IndexViewModel modelBaseVM = new IndexViewModel(new List<Moon>(), planetModelFilter, new List<Star>());
                return modelBaseVM;
            }
            else if (filter == "Moon")
            {
                var moonModelFilter = _context.Moon.ToList();
                IndexViewModel modelBaseVM = new IndexViewModel(moonModelFilter, new List<Planet>(), new List<Star>());
                return modelBaseVM;
            }
            else
            {
                return null;
            }
        }

    }
}
