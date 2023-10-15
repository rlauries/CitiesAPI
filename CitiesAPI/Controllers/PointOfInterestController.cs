using AutoMapper;
using CitiesAPI.Models;
using CitiesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.Controllers
{
    [Route("api/cities/{cityid}/pointofinterestid")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailServices _mailServices;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,
            IMailServices mailServices,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mailServices = mailServices ??
                throw new ArgumentNullException(nameof(mailServices));
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<PointOfInterestDto>> GetPointsOfInterest(
            int cityId)
        {
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation(
                    $"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            var pointsOfInterestForCity = await _cityInfoRepository
                .GetAllPointsOfInterestAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));


        }
        [HttpGet("{pointOfInterestid}", Name = "getpointofinterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(
            int cityId, int pointOfInterestID)
        {
            if (!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterest = await _cityInfoRepository
                 .GetOnePointsOfInterestForCityAsync(cityId, pointOfInterestID);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));


        }




        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityID, PointOfInterestForCreationDto newPoint)
        {
            if(!await _cityInfoRepository.CityExistAsync(cityID))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(newPoint);
            
            await _cityInfoRepository.AddPointOfInterestForCityAsync(
                cityID, finalPointOfInterest);

            await _cityInfoRepository.SaveChangeAsync();

            //convertimos a un json para mostrarlo en Postman
            var createdPointOfInterestToReturn =
                _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);



            //Y esto q es ???????

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityID = cityID,
                    pointOfInterestId = createdPointOfInterestToReturn.Id 
                },
                createdPointOfInterestToReturn);
        }


        [HttpPut("{pointofinterestid}")]
        
        public async Task<ActionResult> UpdatePointOfInterest(
            int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            if(!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
                .GetOnePointsOfInterestForCityAsync(cityId, pointOfInterestId);
            
            if(pointOfInterestEntity == null)
                return NotFound();

            //aqui mapper.map funciona alreves de como lo aviamos visto antes,
            //pero eso es para mapearlo dentro del mismo objeto
            _mapper.Map(pointOfInterest, pointOfInterestEntity);


            await _cityInfoRepository.SaveChangeAsync();

            return NoContent();

        }

        [HttpPatch("{pointofinterestid}")]

        public async Task<ActionResult> PartiallyUpdatePointOfInterest(
            int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            if(!await _cityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterestEntity = await _cityInfoRepository
                .GetOnePointsOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
                return NotFound();

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(
                pointOfInterestEntity);
            
            //proceso para validar el json 
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            //mapeo  
            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangeAsync();

            return NoContent();

        }

        //        [HttpDelete("{pointofinterestid}")]
        //        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId) 
        //        {
        //            var city = _citiesDataStore.Cities
        //                       .FirstOrDefault(c => c.Id == cityId);
        //            if (city == null)
        //                return NotFound();

        //            var pointOfInterestFromStore = city.PointOfInterests
        //                       .FirstOrDefault(p => p.Id == pointOfInterestId);
        //            if (pointOfInterestFromStore == null)
        //                return NotFound();

        //            city.PointOfInterests.Remove(pointOfInterestFromStore);
        //            _mailServices.Send(
        //                "Point of interest deleted.",
        //                $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id}");
        //            return NoContent();

        //        }


    }
 }
