using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HowartsAPI.DTOs
{
    public class HouseCreationDtos
    {
        [Required(ErrorMessage = "Por favor, ingrese el Id de la casa")]
        [DisplayName("CasaId")]
        public int HouseId { get; set; }
        [Required(ErrorMessage = "Por favor, ingrese el nombre de la casa a la que aspira pertenecer"), MaxLength(10)]
        [DisplayName("Casa")]
        public string NameHouse { get; set; }
    }
}
