using AutoMapper;

namespace CitiesAPI.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile() 
        { 
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            
        
        }
    }
}
