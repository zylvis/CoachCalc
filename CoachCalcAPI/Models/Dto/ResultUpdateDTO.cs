using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachCalcAPI.Models
{
    public class ResultUpdateDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AthleteeId { get; set; }
        public int ExerciseId { get; set; }
        public string Value { get; set; }
        public string Date { get; set; }
    }
}
