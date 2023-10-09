using CitiesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesControllers : ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;

        public CitiesControllers(CitiesDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }
        [HttpGet]
        public ActionResult<CityDto> GetCities()
        {
            return Ok(_citiesDataStore.Cities);
        }
        
        [HttpGet("allpoints")]
        public ActionResult<PointOfInterestDto> GetAllPointsOfInterest()
        {
            var allPointOfInterest = new List<PointOfInterestDto>();
            int cityCount = _citiesDataStore.Cities.Count;
            for (int i = 0; i < cityCount; i++)
            {
                int pOfICount = _citiesDataStore.Cities[i].PointOfInterests.Count;
                for (int j = 0; j < pOfICount; j++)
                {
                    allPointOfInterest.Add(_citiesDataStore.Cities[i].PointOfInterests[j]);
                }

            }
            return Ok(allPointOfInterest);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCityById(int id)
        {
            var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
        }



    }
}
