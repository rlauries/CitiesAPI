using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitiesAPI.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        //Relation between classes is created when there...
        //...is a naviation property between classes (City city puntero)
        [ForeignKey("CityId")]
        public City? City { get; set; }
                
        public int CityId { get; set; }

        public PointOfInterest(string name)
        {
            Name = name;
        }

    }
}