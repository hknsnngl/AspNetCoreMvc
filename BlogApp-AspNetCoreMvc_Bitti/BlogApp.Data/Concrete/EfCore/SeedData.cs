using BlogApp.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void Seed (IApplicationBuilder app)
        {
            BlogContext context = app.ApplicationServices.GetRequiredService<BlogContext>();

            context.Database.Migrate();              //VT nın oluştuğu an.

            if (!context.Categories.Any())           //Herhangi Bir kayıt yoksa.
            {
                context.Categories.AddRange(
                    new Category() { Name="Category 1"},
                    new Category() { Name = "Category 2" },
                    new Category() { Name = "Category 3" }
                 );
                context.SaveChanges();
            }


            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(
                    new Blog() { Title = "T1", Description = "D1", Body = "B1", Image = "r1.jpg", Date = DateTime.Now.AddDays(-5), isApproved=true, CategoryId=1 },
                    new Blog() { Title = "T2", Description = "D2", Body = "B2", Image = "r2.jpg", Date = DateTime.Now.AddDays(-6), isApproved = true, CategoryId = 1 },
                    new Blog() { Title = "T3", Description = "D3", Body = "B3", Image = "r3.jpg", Date = DateTime.Now.AddDays(-7), isApproved = false, CategoryId = 2 },
                    new Blog() { Title = "T4", Description = "D4", Body = "B4", Image = "r4.jpg", Date = DateTime.Now.AddDays(-8), isApproved = true, CategoryId = 3 }
                 );
                context.SaveChanges();
            }
        }


    }
}
