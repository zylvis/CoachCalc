﻿namespace CoachCalcAPI.Models.Dto
{
    public class AthleteeDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string BirthDate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? Image { get; set; }
    }
}
