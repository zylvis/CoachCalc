using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachCalcAPI.Models.Dto
{
    public class AthleteeCreateDTO
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Image { get; set; }
    }
}
