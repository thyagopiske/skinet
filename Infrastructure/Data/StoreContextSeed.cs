﻿using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();

            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    await context.ProductBrands.AddRangeAsync(brands);

                    await context.SaveChangesAsync();

                    logger.LogInformation("ProductBrands seed done!");
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    await context.ProductTypes.AddRangeAsync(types);

                    await context.SaveChangesAsync();

                    logger.LogInformation("ProductTypes seed done!");
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    await context.Products.AddRangeAsync(products);

                    await context.SaveChangesAsync();

                    logger.LogInformation("Products seed done!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
