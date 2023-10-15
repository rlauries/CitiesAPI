﻿namespace CitiesAPI.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

       public List<PointOfInterestDto> PointOfInterests { get; set;}
        = new List<PointOfInterestDto>();



    }
}
