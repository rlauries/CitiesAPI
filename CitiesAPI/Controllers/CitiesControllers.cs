using AutoMapper;
using CitiesAPI.Models;
using CitiesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesControllers : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesControllers(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetAllCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities));
                                
            
            
            // return Ok(_citiesDataStore.Cities);

        }

        //[HttpGet("allpoints")]
        //public ActionResult<PointOfInterestDto> GetAllPointsOfInterest()
        //{
        //    var allPointOfInterest = new List<PointOfInterestDto>();
        //    int cityCount = _citiesDataStore.Cities.Count;
        //    for (int i = 0; i < cityCount; i++)
        //    {
        //        int pOfICount = _citiesDataStore.Cities[i].PointOfInterests.Count;
        //        for (int j = 0; j < pOfICount; j++)
        //        {
        //            allPointOfInterest.Add(_citiesDataStore.Cities[i].PointOfInterests[j]);
        //        }

        //    }
        //    return Ok(allPointOfInterest);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(
            int id, bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetOneCityAsync(id, includePointsOfInterest);
            if(city == null)
            {
                return NotFound();
            }
            if(includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));


            return Ok();
        }



    }
}
