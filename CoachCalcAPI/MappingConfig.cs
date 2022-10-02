using AutoMapper;
using CoachCalcAPI.Models;
using CoachCalcAPI.Models.Dto;

namespace CoachCalcAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Exercise, ExerciseDTO>().ReverseMap();
            CreateMap<Exercise, ExerciseCreateDTO>().ReverseMap();
            CreateMap<Exercise, ExerciseUpdateDTO>().ReverseMap();
        }
    }
}
