using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace IdentityServer.Data
{
    public class IdentityUserSeedData
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, ILogger<IdentityUserSeedData> logger)
        {
            if (!dbContext.Roles.Any())
            {
                string[] roles = { "Admin", "User", "Auditor" };
                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(dbContext);

                    if (!dbContext.Roles.Any(r => r.Name == role))
                    {
                        roleStore.CreateAsync(new IdentityRole(role));
                    }
                }

                logger.LogInformation("Seed database associated with context {DbContextName} --> user role", typeof(ApplicationDbContext).Name);
            }
        }
    }
}
