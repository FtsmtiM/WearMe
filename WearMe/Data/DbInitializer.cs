using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;

namespace WearMe.Data
{
    public class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            //AppDbContext context =
            //applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();

            context.Database.EnsureCreated();

            if (!context.Categories.Any())
            {
                var genresList = new Category[]
                {
                    new Category()
                    { CategoryId = 1, CategoryName = "T-Shirts/Top", Desciption = "Daily wear" },
                    new Category()
                    { CategoryId = 2, CategoryName = "Dress/Skirt" },
                    new Category()
                    { CategoryId = 3, CategoryName = "NightWear" },
                    new Category()
                    { CategoryId = 4, CategoryName = "Casual" }
                };
                foreach (Category genre in genresList)
                {
                    context.Categories.Add(genre);
                }
                context.SaveChanges();
            }


            if (!context.SexGroups.Any())
            {
                var genresList = new Sex[]
                {
                    new Sex()
                    { SexId = 1, SexGroup = "Women" },
                    new Sex()
                    { SexId = 2, SexGroup = "Men" },
                    new Sex()
                    { SexId = 3, SexGroup = "Boy" },
                    new Sex()
                    { SexId = 4, SexGroup = "Girl" }
                };
                foreach (Sex genre in genresList)
                {
                    context.SexGroups.Add(genre);
                }
                context.SaveChanges();
            }


            if (!context.SexCategories.Any())
            {
                var genresList = new SexCategory[]
               {
                   new SexCategory()
                    {CategoryId = 1, SexId = 1 },
                    new SexCategory()
                    {CategoryId = 2, SexId = 1 },
                    new SexCategory()
                    {CategoryId = 3, SexId = 1 },
                    new SexCategory()
                    {CategoryId = 4, SexId = 1 },
                    new SexCategory()
                    {CategoryId = 1, SexId = 2 },
                    new SexCategory()
                    {CategoryId = 2, SexId = 2 },
                    new SexCategory()
                    {CategoryId = 3, SexId = 2 },
                    new SexCategory()
                    {CategoryId = 4, SexId = 2 },
                    new SexCategory()
                    {CategoryId = 2, SexId = 3 },
                    new SexCategory()
                    {CategoryId = 3, SexId = 3 },
                    new SexCategory()
                    {CategoryId = 4, SexId = 3 },
                    new SexCategory()
                    {CategoryId = 1, SexId = 4 },
                    new SexCategory()
                    {CategoryId = 4, SexId = 4}
               };
                foreach (SexCategory genre in genresList)
                {
                    context.SexCategories.Add(genre);
                }
                context.SaveChanges();
            }

            if (!context.Clothes.Any())
            {
                var genresList = new Cloth[]
                {
                    new Cloth
                    { Name = "NavyBlue", CategoryId = 3, SexId = 3,
                     Price=400.00M , IsTrending=true, InStock=true,
                     Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Sunshinekit", CategoryId = 2, SexId = 1,
                      Price=400.00M , IsTrending=false, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Navy", CategoryId = 4, SexId = 2,
                      Price=400.00M , IsTrending=true, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Rubby", CategoryId = 4, SexId = 2,
                      Price=400.00M , IsTrending=true, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Doooo", CategoryId = 1, SexId = 1,
                      Price=400.00M , IsTrending=false, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Champ", CategoryId = 3, SexId = 3,
                      Price=400.00M , IsTrending=false, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Sunrise", CategoryId = 2, SexId = 1,
                      Price=400.00M , IsTrending=true, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Coolkk", CategoryId = 4, SexId = 3,
                      Price=400.00M , IsTrending=true, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Ramp", CategoryId = 1, SexId = 4,
                      Price=400.00M , IsTrending=false, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."},
                    new Cloth
                    { Name = "Kuku", CategoryId = 3, SexId = 1,
                      Price=400.00M , IsTrending=true, InStock=true,
                      Description="High waist trousers with adjustable tied hems and invisible side zip fastening."}
                };
                foreach (Cloth genre in genresList)
                {
                    context.Clothes.Add(genre);
                }
                context.SaveChanges();
            }
        }

        //private static Dictionary<string, Category> categories;
        //public static Dictionary<string, Category> Categories
        //{
        //    get
        //    {
        //        if (categories == null)
        //        {
        //            var genresList = new Category[]
        //            {
        //                new Category()
        //                { CategoryId = 1, CategoryName = "T-Shirts/Top", Desciption = "Daily wear" },
        //                new Category()
        //                { CategoryId = 2, CategoryName = "Dress/Skirt" },
        //                new Category()
        //                { CategoryId = 3, CategoryName = "NightWear" },
        //                new Category()
        //                { CategoryId = 4, CategoryName = "Casual" }
        //            };

        //            categories = new Dictionary<string, Category>();

        //            foreach (Category genre in genresList)
        //            {
        //                categories.Add(genre.CategoryName, genre);
        //            }
        //        }

            //    return categories;
            //}
        //}

        //private static Dictionary<string, Sex> sexes;
        //public static Dictionary<string, Sex> Sexes
        //{
        //    get
        //    {
        //        if (sexes == null)
        //        {
        //            var genresList = new Sex[]
        //            {
        //                new Sex()
        //                { SexId = 1, SexGroup = "Women" },
        //                new Sex()
        //                { SexId = 2, SexGroup = "Men" },
        //                new Sex()
        //                { SexId = 3, SexGroup = "Boy" },
        //                new Sex()
        //                { SexId = 4, SexGroup = "Girl" }
        //            };
        //            sexes = new Dictionary<string, Sex>();

        //            foreach (Sex genre in genresList)
        //            {
        //                sexes.Add(genre.SexGroup, genre);
        //            }
        //        }

        //        return sexes;
        //    }
        //}

        //private static Dictionary<string, SexCategory> sexcategories;
        //public static Dictionary<string, SexCategory> SexCategories
        //{
        //    get
        //    {
        //        if (sexcategories == null)
        //        {
        //            var genresList = new SexCategory[]
        //            {
        //        //new SexCategory()
        //        //{Category = Categories["1"], Sex = Sexes["1"] },
        //        //new SexCategory()
        //        //{Category = Categories["1"], Sex =Sexes["2"] },
        //        //new SexCategory()
                //{Category =Categories["1"], Sex =Sexes["3"] },
                //new SexCategory()
                //{Category = Categories["1"], Sex= Sexes["4"] },
                //new SexCategory()
                //{Category =Categories["1"], Sex = Sexes["2"] },
                //new SexCategory()
                //{Category =Categories["2"], Sex = Sexes["1"] },
                //new SexCategory()
                //{Category =Categories["2"], Sex = Sexes["4"] },
                //new SexCategory()
                //{Category =Categories["3"], Sex = Sexes["1"] },
                //new SexCategory()
                //{Category = Categories["3"], Sex =Sexes["2"] },
            //    new SexCategory()
            //    { SexCategoryId = 1, CategoryId = 3, SexId = 3 },
            //    new SexCategory()
            //    { SexCategoryId = 2, CategoryId = 3, SexId = 4 },
            //    new SexCategory()
            //    { SexCategoryId = 3, CategoryId = 4, SexId = 1 },
            //    new SexCategory()
            //    { SexCategoryId = 4, CategoryId = 4, SexId = 2 },
            //    new SexCategory()
            //    { SexCategoryId = 5, CategoryId = 4, SexId = 3 }
            //};
            //        sexcategories = new Dictionary<string, SexCategory>();

            //        foreach (SexCategory genre in genresList)
            //        {
            //            sexcategories.Add(genre.SexCategoryId.ToString(), genre);
            //        }
            //    }

            //    return sexcategories;
            //}
        //}

    }
}
