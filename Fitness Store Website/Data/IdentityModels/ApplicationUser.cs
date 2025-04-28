using Microsoft.AspNetCore.Identity;

namespace Fitness_Store_Website.Data.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
