using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachCalcAPI.Models.Dto
{
    public class ResultCreateDTO
    {
        public int AthleteeId { get; set; }
    
        public int ExerciseId { get; set; }
        public string Value { get; set; }
        public string Date { get; set; }
    }
}
