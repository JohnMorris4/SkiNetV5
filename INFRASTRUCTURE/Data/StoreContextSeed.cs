using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CORE.Entities;
using Microsoft.Extensions.Logging;

namespace INFRASTRUCTURE.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory logger)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = 
                        File.ReadAllText("../INFRASTRUCTURE/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                
                if (!context.ProductTypes.Any())
                {
                    var typesData = 
                        File.ReadAllText("../INFRASTRUCTURE/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                
                if (!context.Products.Any())
                {
                    var productsData = 
                        File.ReadAllText("../INFRASTRUCTURE/Data/SeedData/products.json");

                    var product = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in product)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var loggerFactory = logger.CreateLogger<StoreContextSeed>();
                loggerFactory.LogError(e.Message);
            }
        }
    }
}