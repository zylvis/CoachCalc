using Microsoft.AspNetCore.Identity;

namespace CoachCalcAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
