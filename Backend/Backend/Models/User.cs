using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class User: IdentityUser<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}