using Entity_Study_3.Entities;

namespace Entity_Study_3
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.SqlServer;
    using System.Data.SqlClient;
    using static Console;

    internal class Program
    {
        static async Task CreateDatabase()
        {
            using (var dbContext = new ShopContext())
            {
                bool created = await dbContext.Database.EnsureCreatedAsync();
                WriteLine($"Database {(created ? "created" : "already exists")}.");
            }
        }

        static async Task DropDatabase()
        {
            using (var dbContext = new ShopContext())
            {
                bool removed = await dbContext.Database.EnsureDeletedAsync();
                WriteLine($"Database {(removed ? "removed" : "does not exist")}.");
            }
        }

        static async Task AddProduct()
        {
            using (var dbContext = new ShopContext())
            {
                Write("Enter the number of Products you want to add: ");
                if (!int.TryParse(ReadLine(), out int numberToAdd) || numberToAdd <= 0)
                {
                    WriteLine("Invalid input. Please enter a positive integer.");
                    return;
                }

                var categories = await dbContext.Categories.ToListAsync();
                if (categories.Count == 0)
                {
                    WriteLine("No categories found. Please add categories first.");
                    return;
                }

                for (int i = 0; i < numberToAdd; i++)
                {
                    Write("Enter Product Name: ");
                    var productName = ReadLine();
                    if (string.IsNullOrWhiteSpace(productName))
                    {
                        WriteLine("Product name cannot be empty. Skipping this product.");
                        continue;
                    }

                    Write("Enter Price: ");
                    if (!decimal.TryParse(ReadLine(), out decimal productPrice) || productPrice <= 0)
                    {
                        WriteLine("Invalid price. Price must be a positive number. Skipping this product.");
                        continue;
                    }

                    WriteLine("Available Categories:");
                    for (int j = 0; j < categories.Count; j++)
                    {
                        WriteLine($"{j + 1}. {categories[j].Name}");
                    }

                    Write("Enter the number of the category for this product: ");
                    if (!int.TryParse(ReadLine(), out int categoryChoice) || categoryChoice < 1 ||
                        categoryChoice > categories.Count)
                    {
                        WriteLine("Invalid category choice. Skipping this product.");
                        continue;
                    }

                    var selectedCategory = categories[categoryChoice - 1];

                    var product = new Product
                    {
                        Name = productName,
                        Price = productPrice,
                        Category = selectedCategory
                    };

                    dbContext.Products.Add(product);
                    WriteLine($"Product '{productName}' added to category '{selectedCategory.Name}'.");
                }

                int rowsAffected = await dbContext.SaveChangesAsync();
                WriteLine($"{rowsAffected} row(s) added to Database");
            }
        }

        static async Task AddCategory()
        {
            using (var dbContext = new ShopContext())
            {
                Write("Enter the number of Category want to add: ");
                var numberadd = Convert.ToInt32(ReadLine());
                for (int i = 0; i < numberadd; i++)
                {
                    Write("Enter Category Name: ");
                    var CategoryName = ReadLine();
                    Write("Enter Description: ");
                    var CategoryDiscription = ReadLine();
                    var category = new Category
                    {
                        Discription = CategoryDiscription,
                        Name = CategoryName
                    };
                    await dbContext.Categories.AddAsync(category);
                    int saveChangesAsync = await dbContext.SaveChangesAsync();
                    if (saveChangesAsync == 1)
                    {
                        WriteLine("1 row add to Database");
                    }
                    else WriteLine("0 row add to Database");
                }
            }
        }

        static async Task Main(string[] args)
        {
            await CreateDatabase();
            await AddCategory();
            await AddProduct();
        }
    }
}