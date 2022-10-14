using AutoMapper;
using CoachCalcAPI.Models;
using CoachCalcAPI.Models.Dto;

namespace CoachCalcAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Athletee, AthleteeDTO>().ReverseMap();
            CreateMap<Athletee, AthleteeCreateDTO>().ReverseMap();
            CreateMap<Athletee, AthleteeUpdateDTO>().ReverseMap();

            CreateMap<Exercise, ExerciseDTO>().ReverseMap();
            CreateMap<Exercise, ExerciseCreateDTO>().ReverseMap();
            CreateMap<Exercise, ExerciseUpdateDTO>().ReverseMap();

            CreateMap<Result, ResultDTO>().ReverseMap();
            CreateMap<Result, ResultCreateDTO>().ReverseMap();
            CreateMap<Result, ResultUpdateDTO>().ReverseMap();


            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
    
}
