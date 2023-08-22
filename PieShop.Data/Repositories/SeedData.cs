using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PieShop.Core.Models;
using PieShop.Data;
using PieShop.Data.Reposities;

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

            //var pies = GetPieRepository();
            //await db.AddRangeAsync(pies);

            //var catogory = GetCategoryRepository();
            //await db.AddRangeAsync(catogory);

            //var cat = Category();
            //await db.AddRangeAsync(cat);

            //var movies = GenerateMovies(1, cinamas, producers);
            //await db.AddRangeAsync(movies);

            //var enrollments = GenerateEnrollments(actors, movies);
            //await db.AddRangeAsync(enrollments);

            await db.SaveChangesAsync();
        }


        //public static Mock<IPieRepository> GetPieRepository()
        //{
        //    var pies = new List<Pie>
        //    {
        //        new Pie
        //        {
        //            Name = "Apple Pie",
        //            Price = 12.95M,
        //            ShortDescription = "Our famous apple pies!",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Fruit pies"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = true,
        //            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Blueberry Cheese Cake",
        //            Price = 18.95M,
        //            ShortDescription = "You'll love it!",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Cheese cakes"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecake.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = false,
        //            ImageThumbnailUrl =
        //                "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Cheese Cake",
        //            Price = 18.95M,
        //            ShortDescription = "Plain cheese cake. Plain pleasure.",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Cheese cakes"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = false,
        //            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Cherry Pie",
        //            Price = 15.95M,
        //            ShortDescription = "A summer classic!",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Fruit pies"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = false,
        //            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Christmas Apple Pie",
        //            Price = 13.95M,
        //            ShortDescription = "Happy holidays with this pie!",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Seasonal pies"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepie.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = false,
        //            ImageThumbnailUrl =
        //                "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepiesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Cranberry Pie",
        //            Price = 17.95M,
        //            ShortDescription = "A Christmas favorite",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Seasonal pies"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypie.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = false,
        //            ImageThumbnailUrl =
        //                "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypiesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Peach Pie",
        //            Price = 15.95M,
        //            ShortDescription = "Sweet as peach",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Fruit pies"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpie.jpg",
        //            InStock = false,
        //            IsPieOfTheWeek = false,
        //            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Pumpkin Pie",
        //            Price = 12.95M,
        //            ShortDescription = "Our Halloween favorite",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Seasonal pies"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = true,
        //            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Rhubarb Pie",
        //            Price = 15.95M,
        //            ShortDescription = "My God, so sweet!",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Fruit pies"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = true,
        //            ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //        new Pie
        //        {
        //            Name = "Strawberry Pie",
        //            Price = 15.95M,
        //            ShortDescription = "Our delicious strawberry pie!",
        //            LongDescription =
        //                "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            Category = Categories["Fruit pies"],
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg",
        //            InStock = true,
        //            IsPieOfTheWeek = false,
        //            ImageThumbnailUrl =
        //                "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg",
        //            AllergyInformation = ""
        //        },
        //    };

        //    var mockPieRepository = new Mock<IPieRepository>();
        //    mockPieRepository.Setup(repo => repo.Pies).Returns(pies);
        //    mockPieRepository.Setup(repo => repo.GetPieById(It.IsAny<int>())).Returns(pies[0]);
        //    return mockPieRepository;
        //}

        //public static Mock<ICategoryRepository> GetCategoryRepository()
        //{
        //    var categories = new List<Category>
        //    {
        //        new Category()
        //        {
        //            CategoryId = 1,
        //            CategoryName = "Fruit Pies",
        //            Description = "Lorem ipsum"
        //        },
        //        new Category()
        //        {
        //            CategoryId = 2,
        //            CategoryName = "Cheese cakes",
        //            Description = "Lorem ipsum"
        //        },
        //        new Category()
        //        {
        //            CategoryId = 3,
        //            CategoryName = "Fruit Pies",
        //            Description = "Seasonal pies"
        //        }
        //    };

        //    var mockCategoryRepository = new Mock<ICategoryRepository>();
        //    mockCategoryRepository.Setup(repo => repo.Categories).Returns(categories);

        //    return mockCategoryRepository;
        //}

        //private static Dictionary<string, Category> _categories;
        //public static Dictionary<string, Category> Categories
        //{
        //    get
        //    {
        //        if (_categories == null)
        //        {
        //            var genresList = new Category[]
        //            {
        //                new Category { CategoryName = "Fruit pies" },
        //                new Category { CategoryName = "Cheese cakes" },
        //                new Category { CategoryName = "Seasonal pies" }
        //            };

        //            _categories = new Dictionary<string, Category>();

        //            foreach (var genre in genresList)
        //            {
        //                _categories.Add(genre.CategoryName, genre);
        //            }
        //        }

        //        return _categories;

        //    }
        //}
    }
}




