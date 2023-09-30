using CitiesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesControllers : ControllerBase
    {
        [HttpGet]
        public ActionResult<CityDto> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }
        
        [HttpGet("allpoints")]
        public ActionResult<PointOfInterestDto> GetAllPointsOfInterest()
        {
            var allPointOfInterest = new List<PointOfInterestDto>();
            int cityCount = CitiesDataStore.Current.Cities.Count;
            for (int i = 0; i < cityCount; i++)
            {
                int pOfICount = CitiesDataStore.Current.Cities[i].PointOfInterests.Count;
                for (int j = 0; j < pOfICount; j++)
                {
                    allPointOfInterest.Add(CitiesDataStore.Current.Cities[i].PointOfInterests[j]);
                }

            }
            return Ok(allPointOfInterest);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCityById(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
        }



    }
}
