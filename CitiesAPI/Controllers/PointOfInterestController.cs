﻿using CitiesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.Controllers
{
    [Route("api/cities/{cityid}/pointofinterests")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterests);

        }
        [HttpGet("{pointOfInterestID}")]
        public ActionResult <PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestID)
        { 
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
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


    }
}
