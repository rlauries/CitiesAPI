using CitiesAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitiesAPI.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {

        }
        //give access to Model Builder

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("New York City")
                {
                    Id = 1,
                    Description = "The One with that big park."
                }, 
                new City("Antwerp")
                {
                    Id = 2,
                    Description = "The one with the cathedral that was never finished."
                }, 
                new City("Paris")
                {
                    Id = 3,
                    Description = "The One with that big tower."
                });
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "The most visited urban park in the US"
                },
                new PointOfInterest("Empire State Building.")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "A 102-story skyscraper located in Midtown Manhattan."
                },
                new PointOfInterest("Cathedral")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "A gothic style  cathedral."
                }, 
                new PointOfInterest("Antwerp Central Station")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "Railway architecture in Belgium"
                },
                new PointOfInterest("Eiffel Tower")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "A wrought iron lattice tower in the Champ de Mars."
                },
                new PointOfInterest("The Louvre")
                {
                    Id = 6,
                    CityId = 3,
                    Description = "The world's largest museum."
                }
                ); 


            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
