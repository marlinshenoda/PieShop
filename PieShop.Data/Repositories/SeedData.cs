using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Core.Models;
using PieShop.Data;

namespace PieShop.Data.Repositories
{
    public class SeedData
    {
        private static ApplicationDbContext db = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<ApplicationUser> userManager = default!;


        private static async Task AddToRolesAsync(ApplicationUser admin, string[] roleNames)
        {
            foreach (var role in roleNames)
            {
                if (await userManager.IsInRoleAsync(admin, role)) continue;
                var result = await userManager.AddToRoleAsync(admin, role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task<ApplicationUser> AddAdminAsync(string adminEmail, string adminPW)
        {
            var found = await userManager.FindByEmailAsync(adminEmail);

            if (found != null) return null!;

            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
            };

            var result = await userManager.CreateAsync(admin, adminPW);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return admin;
        }
        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        public static async Task InitAsync(ApplicationDbContext db, IServiceProvider services, string adminPW)
        {
            //if (await db.Actors.AnyAsync()) return;

            //faker = new Faker("sv");
            if (db is null) throw new ArgumentNullException(nameof(db));

            ArgumentNullException.ThrowIfNull(nameof(services));
            if (services is null) throw new ArgumentNullException(nameof(services));

            // if (db.app.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            ArgumentNullException.ThrowIfNull(roleManager);

            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            ArgumentNullException.ThrowIfNull(userManager);

            var roleNames = new[] { "Member", "Admin" };
            var adminEmail = "admin@etickets.se";


            await AddRolesAsync(roleNames);

            var admin = await AddAdminAsync(adminEmail, adminPW);

            await AddToRolesAsync(admin, roleNames);

            //var actors = GenerateActors(6);
            //await db.AddRangeAsync(actors);

            //var producers = GenerateProducers(6);
            //await db.AddRangeAsync(producers);

            //var cinamas = GenerateCinemas(5);
            //await db.AddRangeAsync(cinamas);

            //var movies = GenerateMovies(1, cinamas, producers);
            //await db.AddRangeAsync(movies);

            //var enrollments = GenerateEnrollments(actors, movies);
            //await db.AddRangeAsync(enrollments);

            await db.SaveChangesAsync();
        }
    }
}
