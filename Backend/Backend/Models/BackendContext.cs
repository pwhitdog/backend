using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class BackendContext: IdentityDbContext
    {
        public BackendContext(DbContextOptions<BackendContext> options) : base(options)
        {
        }
    }

    public class DbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DbInitializer(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task Initialize(BackendContext context)
        {
            IdentityRole admin;
            
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                admin = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                };
                context.Roles.Add(admin);
                await context.SaveChangesAsync();
            }
            else
            {
                admin = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            }

            IdentityUser user;
            if (!context.Users.Any(u => u.Email == "admin@nope.com"))
            {
                user = new IdentityUser
                {
                    Email = "admin@nope.com",
                    NormalizedEmail = "admin@nope.com",
                    UserName = "Admin"
                };
                var result = await _userManager.CreateAsync(user, "herpDerp1!");
            }
            else
            {
                user = await context.Users.FirstOrDefaultAsync(u => u.Email == "admin@nope.com");
            }
            var derp = await _userManager.AddToRoleAsync(user, admin.Name);

        }
    }
}