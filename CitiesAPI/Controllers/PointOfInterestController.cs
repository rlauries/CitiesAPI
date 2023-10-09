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
        private readonly CitiesDataStore _citiesDataStore;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,
            IMailServices mailServices,
            CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailServices = mailServices ?? throw new ArgumentNullException(nameof(mailServices));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore)); 
        }

        [HttpGet]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId)
        {

            try
            {
                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation(
                        $"City with if {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                return Ok(city.PointOfInterests);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting points of interest for city with Id {cityId}.",
                    ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }


           

        }
        [HttpGet("{pointOfInterestID}")]
        public ActionResult <PointOfInterestDto> GetPointOfInterest(
            int cityId, int pointOfInterestID)
        { 
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            var pointOfInterest = city.PointOfInterests.FirstOrDefault(p => p.Id == pointOfInterestID);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        
        }

        


        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityID, PointOfInterestForCreationDto newPoint)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => cityID == c.Id);
            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(
                c => c.PointOfInterests).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = maxPointOfInterestId++,
                Name = newPoint.Name,
                Description = newPoint.Description

            };

            city.PointOfInterests.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityID,
                    pointOfInterestId = finalPointOfInterest.Id
                },
                finalPointOfInterest);

        }

        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => cityId == c.Id);
            if(city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == pointOfInterestId);
            if(pointOfInterestFromStore == null)
                return NotFound();

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;
           
            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]

        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = _citiesDataStore.Cities
                       .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointOfInterestFromStore = city.PointOfInterests
                       .FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
                return NotFound();

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description

            };
            
            //ModelState validate the parameters 

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //Validate ModelState
            if(!TryValidateModel(pointOfInterestToPatch)) 
                return BadRequest(ModelState);

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();

        }

        [HttpDelete("{pointofinterestid}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId) 
        {
            var city = _citiesDataStore.Cities
                       .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            var pointOfInterestFromStore = city.PointOfInterests
                       .FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
                return NotFound();

            city.PointOfInterests.Remove(pointOfInterestFromStore);
            _mailServices.Send(
                "Point of interest deleted.",
                $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id}");
            return NoContent();

        }


    }
}
