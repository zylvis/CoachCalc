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

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public string Value { get; set; }
        public DateTime TimeValue { get; set; }
        public DateTime Date { get; set; }

    }
}
