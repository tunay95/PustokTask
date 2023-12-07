using Microsoft.AspNetCore.Identity;

namespace WebAppRelation.Models
{
    public class AppUser : IdentityUser 
    {
        public string Fullname { get; set; }
        public bool IsRemained { get; set; }

    }
}
