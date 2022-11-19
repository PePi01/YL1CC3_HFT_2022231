using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using YL1CC3_HFT_2022231.Logic;
using YL1CC3_HFT_2022231.Models;
using YL1CC3_HFT_2022231.Repository;

namespace YL1CC3_HFT_2022231.Client
{
    class Program
    {

            static RestService rest;

        #region Brand
            static void Create(string entity)
            {
                if (entity == "Brand")
                {
                    Console.Write("Enter Brand Name: ");
                    string name = Console.ReadLine();
                    rest.Post(new Brand() { Name = name }, "brand");
                }
            }
            static void List(string entity)
            {
                if (entity == "Brand")
                {
                    List<Brand> brands = rest.Get<Brand>("brand");
                    foreach (var item in brands)
                    {
                        Console.WriteLine(item.Id + ": " + item.Name);
                    }
                }
                Console.ReadLine();
            }
            static void Update(string entity)
            {
                if (entity == "Brand")
                {
                    Console.Write("Enter Brand's id to update: ");
                    int id = int.Parse(Console.ReadLine());
                    Brand one = rest.Get<Brand>(id, "brand");
                    Console.Write($"New name [old: {one.Name}]: ");
                    string name = Console.ReadLine();
                    one.Name = name;
                    rest.Put(one, "brand");
                }
            }
            static void Delete(string entity)
            {
                if (entity == "Brand")
                {
                    Console.Write("Enter Brand's id to delete: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "brand");
                }
            }
        #endregion

        static void Main(string[] args)
            {
                rest = new RestService("http://localhost:10237/","brand");

                var brandSubMenu = new ConsoleMenu(args, level: 1)
                    .Add("List", () => List("Brand"))
                    .Add("Create", () => Create("Brand"))
                    .Add("Delete", () => Delete("Brand"))
                    .Add("Update", () => Update("Brand"))
                    .Add("Exit", ConsoleMenu.Close);

                var rentSubMenu = new ConsoleMenu(args, level: 1)
                    .Add("List", () => List("Rent"))
                    .Add("Create", () => Create("Rent"))
                    .Add("Delete", () => Delete("Rent"))
                    .Add("Update", () => Update("Rent"))
                    .Add("Exit", ConsoleMenu.Close);

                var carSubMenu = new ConsoleMenu(args, level: 1)
                    .Add("List", () => List("Car"))
                    .Add("Create", () => Create("Car"))
                    .Add("Delete", () => Delete("Car"))
                    .Add("Update", () => Update("Car"))
                    .Add("Exit", ConsoleMenu.Close);




                var menu = new ConsoleMenu(args, level: 0)
                    .Add("Car", () => carSubMenu.Show())
                    .Add("Brands", () => brandSubMenu.Show())
                    .Add("Rent", () => rentSubMenu.Show())
                    .Add("Exit", ConsoleMenu.Close);

                menu.Show();

            }

            //CarDbContext db = new CarDbContext();
            ////var cars = new CarRepository(db);
            ////var rents = new RentRepository(db);
            ////var brands = new BrandRepository(db);

            //var carLogic = new CarLogic(new CarRepository(db));
            //var rentLogic = new RentLogic(new RentRepository(db));
            //var brandLogic = new BrandLogic(new BrandRepository(db));


            //var asd = from x in db.Rents.AsEnumerable()
            //          group x by x.CarId into g
            //          select new
            //          {
            //              Frequency = g.Count(),
            //              Model = g.Select(t => t.Car.Model),
            //          };
            //var asd2 = from x in new RentRepository(db).ReadAll().AsEnumerable()
            //           group x by x.CarId into g
            //           select new
            //           {
            //               Frequency = g.Count(),
            //               Model = g.Select(t => t.Car.Model),
            //           };

            //var nemar = new CarRepository(db).ReadAll().Where(t => t.Rents.FirstOrDefault() != null);
            //var res1 = from m in db.Brands
            //           group m by m.Id into g
            //           select new
            //           {

            //               Auto=db.Cars,
            //           };

            //var carrrr = db.Cars.ToList();
            //var nem = from x in db.Cars
            //          select x;
            //var idk = from x in db.Cars.AsEnumerable()
            //          group x by x.BrandId into g
            //          select new
            //          {
            //              Brand=g.Select(t=>t.Brand.Name),
            //              Model = g.Select(t => t.Model),
            //          };
            ////crud1
            //var price = from x in new CarRepository(db).ReadAll().AsEnumerable()
            //            group x by x.Brand into g
            //            select new
            //            {
            //                Brand = g.Key.Name,
            //                Sum = g.Sum(t => t.Price)
            //            };
            ////var seg=from x in rents.ReadAll().AsEnumerable()
            ////group x by x.CarId into g
            ////select new 
            ////{
            ////    Frequency = g.Count(),
            ////    Model = g.Select(t => t.Car.Model),
            ////};
            //// NON CRUDOK 
            //var segg = carLogic.FreqOfCarsRented();
            //var fos = brandLogic.SumPriceByBrand();
            //var matuka = rentLogic.RentTimes();
            //var haziko = brandLogic.FreqOfBrandsRented();
            //var helmet = brandLogic.AvgPriceByBrand();




        
    }
}
