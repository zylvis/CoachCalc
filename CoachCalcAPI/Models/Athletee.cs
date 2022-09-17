using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachCalcAPI.Models
{
    public class Athletee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? BirthDate { get; set; }
        public string? Image { get; set; }
        public string SearchColumn
        {
            get { return $"{FirstName}{LastName}"; }
        }


    }
}
