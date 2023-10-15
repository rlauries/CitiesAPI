using CitiesAPI.Entities;

namespace CitiesAPI.Services
{
    public interface ICityInfoRepository
    {

        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City> GetOneCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetAllPointsOfInterestAsync(int cityId);
        Task<PointOfInterest> GetOnePointsOfInterestForCityAsync(int cityId,
            int pointOfInterestId);

        Task<bool> CityExistAsync(int cityId);
        Task AddPointOfInterestForCityAsync(
            int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangeAsync();

    }
}
