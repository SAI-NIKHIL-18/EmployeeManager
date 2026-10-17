using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.DBContext
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "b2f9c918-b40f-4303-a83c-a173ec14dd2e";
            var writerId = "b2f9c918-b40f-4303-a83c-a173ec14dd2e";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerId,
                    Name = "Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp = readerId
                },
                new IdentityRole
                {
                    Id = writerId,
                    Name = "Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp = writerId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
