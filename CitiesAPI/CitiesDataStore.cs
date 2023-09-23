using CitiesAPI.Models;

namespace CitiesAPI
{
    public class CitiesDataStore
    {

        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with a big park.",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urban Park in US"

                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skyscraper located in Midtown Manhattan"
                        }
                    }

                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with a cathedral.",
                    PointOfInterests  = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto
                        {
                            Id = 3,
                            Name = "Aberowen",
                            Description = "The most visited church"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with a big tower.",
                    PointOfInterests  = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto
                        {
                            Id = 4,
                            Name = "Eiffer Tower",
                            Description = "The most Tower in the World "
                        }
                    }
                }

            };
        }



    }
}
