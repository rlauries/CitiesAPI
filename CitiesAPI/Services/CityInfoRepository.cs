using CitiesAPI.DbContexts;
using CitiesAPI.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace CitiesAPI.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
           return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public Task<IEnumerable<PointOfInterest>> GetAllPointsOfInterestAsync(int cityId)
        {
            throw new NotImplementedException();
        }

        public async Task<City> GetOneCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterests)
                    .Where(c=> c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities.
                Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }


        public async Task<PointOfInterest> GetOnePointsOfInterestForCityAsync(
            int cityId, 
            int pointOfInterestId)
        {
            return await _context.PointOfInterests
                .Where(p=> p.CityId ==  cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
        }


    }
}
