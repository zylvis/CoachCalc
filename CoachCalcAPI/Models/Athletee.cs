using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachCalcAPI.Models
{
    public class Athletee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string BirthDate { get; set; }
        public string? Image { get; set; }

    }
}
