using AutoMapper;

namespace CitiesAPI.Profiles

{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointOfInterestDto>();
            CreateMap<Entities.City, Models.CityDto>();

        }
    }
}
