using Microsoft.EntityFrameworkCore;

namespace OpenBank.Application
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
