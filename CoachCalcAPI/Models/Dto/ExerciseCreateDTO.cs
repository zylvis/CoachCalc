using System.ComponentModel.DataAnnotations;

namespace CoachCalcAPI.Models.Dto
{
    public class ExerciseCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string MetricType { get; set; }
    }
}
