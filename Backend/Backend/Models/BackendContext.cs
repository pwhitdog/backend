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
}