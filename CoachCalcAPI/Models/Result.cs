using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachCalcAPI.Models
{
    public class Result
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Athletee")]
        public int AthleteeId { get; set; }
        public Athletee Athletee { get; set; }
        public int ExerciseId { get; set; }
        public string Value { get; set; }
        public string Date { get; set; }

    }
}
