﻿namespace CoachCalcAPI.Models.Dto
{
    public class AthleteeUpdateDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Image { get; set; }
    }
}