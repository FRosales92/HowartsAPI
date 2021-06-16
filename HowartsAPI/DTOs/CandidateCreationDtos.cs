using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HowartsAPI.DTOs
{
    public class CandidateCreationDtos
    {

        [Required(ErrorMessage = "Por favor, ingrese el nombre del alummno"), MaxLength(20)]
        [MinLength(3)]
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Por favor, ingrese el apellido del alummno"), MaxLength(20)]
        [MinLength(3)]
        [DisplayName("Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese la identificación del alummno")]
        [DisplayName("Identificación")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor, ingrese la edad del alummno")]
        [DisplayName("Edad")]
        [Range(0,99)]
        public int Age { get; set; }
        [Required]
        [DisplayName("Casa Id")]
        public int HouseId { get; set; }

    }
}
