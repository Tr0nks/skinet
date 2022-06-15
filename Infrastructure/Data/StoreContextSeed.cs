using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {

        //Este metodo sirve para insertar los registros en las tablas desde un archivo JSON

        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {

            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);

                    }

                    await context.Database.OpenConnectionAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductBrands ON");


                    await context.SaveChangesAsync();

                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductBrands OFF");
                    context.Database.CloseConnection();

                }

                if (!context.ProductType.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductType.Add(item);

                    }
                    await context.Database.OpenConnectionAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductType ON");


                    await context.SaveChangesAsync();

                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductType OFF");
                    context.Database.CloseConnection();

                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in product)
                    {
                        context.Products.Add(item);

                    }

                    await context.SaveChangesAsync();

                }

            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);

            }
            

        }
        
    }
}