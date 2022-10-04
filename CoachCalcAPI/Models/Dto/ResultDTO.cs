using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachCalcAPI.Models.Dto
{
    public class ResultDTO
    {
        public int Id { get; set; }
        public int AthleteeId { get; set; }
        public int ExerciseId { get; set; }
        public string Value { get; set; }
        public DateTime TimeValue { get; set; }
        public DateTime Date { get; set; }

    }
}
