namespace CatalogService.Extensions
{
    using CatalogService.Entities;
    using CatalogService.Infrastructure.Database;
    using Microsoft.EntityFrameworkCore;

    public static class MigrationDatabaseExtension
    {
        public static async Task AddDatabaseMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
            context.Database.Migrate();

            if (context.Products.Any())
            {
                return;
            }

            var category1 = new Category
                {                               
                    Name = "Телевизоры",
                    Url = "tv",
                    Icon = "camera-slr"
                };
            var category2 = new Category
                {                    
                    Name = "Холодильники",
                    Url = "fridge",
                    Icon = "camera-slr"
                };           
            
            Product[] products = [
                new Product
                {                    
                    Category = category1,
                    Title = "Sony 65 Inch 4K Ultra HD TV X80K",
                    Description = "Reproduces over a billion accurate colors resulting in picture quality that is natural and precise, and closer than ever to real life, enhanced by TRILUMINOS Pro.",
                    Image = "https://m.media-amazon.com/images/I/8127maI-DWL._AC_UY218_.jpg",
                    Price = 140m,
                    OriginalPrice = 165m
                },
                new Product
                {                    
                    Category = category1,
                    Title = "LG C3 Series 55-Inch Class OLED evo Smart TV OLED55C3PUA",
                    Description = "Experience the magic of the big screen right from your couch. Every LG OLED comes loaded with Dolby Vision for extraordinary color, contrast and brightness, plus Dolby Atmos* for wrap-around sound. Land in the center of the action with LG's FILMMAKER MODE, allowing you to see films just as the director intended. ",
                    Image = "https://m.media-amazon.com/images/I/61fdYYpVYIL._AC_UY218_.jpg",
                    Price = 170m,
                    OriginalPrice = 189m
                },
                new Product
                {                    
                    Category = category2,
                    Title = "Commercial Cool CCRR4LR 4.0 Cu. Ft Freezer",
                    Description = "Enjoy the sleek throwback design and vibrant color of this COMMERCIAL COOL retro refrigerator (21.5” x 23” x 35.2”) while relishing its modern-day functionality. Complete with 4.0 cubic feet of storage and a stylish chrome plated door handle, our vintage style refrigerator (68.1 lbs.) combines old school charm with contemporary capabilities in three colors—cool red, sleek black, and stunning white. ",
                    Image = "https://m.media-amazon.com/images/I/51C83H1qy6L._AC_UY218_.jpg",
                    Price = 285m,
                    OriginalPrice = 307m
                },
                new Product
                {                    
                    Category = category2,
                    Title = "Coca-Cola Classic Coke Bottle 4L Mini Fridge w/ 12V DC and 110V AC Cords",
                    Description = "Vintage Coca-Cola Style: Featuring the iconic contoured glass bottle first introduced over 100 years ago, this vibrant red mini-fridge is a keystone piece for Coke memorabilia fans and collectors! ",
                    Image = "https://m.media-amazon.com/images/I/61ZnUOtbK7L._AC_SX569_.jpg",
                    Price = 64m,
                    OriginalPrice = 73m
                }];

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
        }
    }
}