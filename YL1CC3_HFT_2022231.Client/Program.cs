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


        static void Create(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand Name: ");
                string name = Console.ReadLine();
                rest.Post(new Brand() { Name = name }, "brand");
            }
            else if (entity=="Rent")
            {
                Rent input = new Rent();
                Console.WriteLine("Enter Rent Start! ");
                Console.Write("Year:");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Month:");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Day:");
                int day = int.Parse(Console.ReadLine());
                input.Start = new DateTime(year,month,day);

                Console.WriteLine("Enter Rent End! ");
                Console.Write("Year:");
                int yeare = int.Parse(Console.ReadLine());
                Console.Write("Month:");
                int monthe = int.Parse(Console.ReadLine());
                Console.Write("Day:");
                int daye = int.Parse(Console.ReadLine());
                input.End = new DateTime(yeare, monthe, daye);
                //rest.Post(new Rent() { Start = new DateTime(yeare,monthe,daye) }, "rent");
                List("Car");
                Console.Write("Enter car id:");
                input.CarId = int.Parse(Console.ReadLine());
                rest.Post(input, "rent");
            }
            else if (entity=="Car")
            {
                Car input = new Car();
                Console.WriteLine("Enter car props: ");
                Console.WriteLine("Model(e.g.: bmw 116d):");
                input.Model = Console.ReadLine();
                Console.WriteLine("Price:");
                input.Price = int.Parse(Console.ReadLine());
                List("Brand");
                Console.WriteLine("Brand id:");
                input.BrandId = int.Parse(Console.ReadLine());
                rest.Post(input, "car");
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
            if (entity == "Rent")
            {
                List<Rent> rents = rest.Get<Rent>("rent");
                foreach (var item in rents)
                {
                    Console.WriteLine(item.Id + ": " + item.Start+"--"+item.End+" Interval Days: "+item.Interval +" Model: "+item.Car.Model);
                }
            }
            if (entity=="Car")
            {
                List<Car> cars = rest.Get<Car>("car");
                foreach (var item in cars)
                {
                    Console.WriteLine(item.Id + ": " + item.Model);
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            // rent update ertelmetlen
            if (entity == "Brand")
            {
                Console.Write("Enter Brand's id to update: ");
                List("Brand");
                int id = int.Parse(Console.ReadLine());
                Brand one = rest.Get<Brand>(id, "brand");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "brand");
            }
            else if (entity=="Car")
            {
                List("Car");
                Console.WriteLine("Enter Car's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Car one = rest.Get<Car>(id, "car");
                Console.WriteLine($"New model [old: {one.Model}] i.g.: bmw 116d:  ");
                one.Model = Console.ReadLine();
                List("Brand");
                Console.WriteLine($"Brand id: ");
                int iden = int.Parse(Console.ReadLine());
                one.BrandId = iden;
                //one.Brand.Name = rest.Get<Brand>(one.BrandId, "brand").Name;
                rest.Put(one, "car");

            }
            else if (entity == "Rent")
            {
                Console.Write("Enter Rent's id to update: ");
                List("Rent");
                int id = int.Parse(Console.ReadLine());
                Rent one = rest.Get<Rent>(id, "rent");
                Console.WriteLine($"New rent [old car id: {one.Car.Id}]: ");
                
                Console.WriteLine("Enter Rent Start! ");
                Console.Write("Year:");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Month:");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Day:");
                int day = int.Parse(Console.ReadLine());
                one.Start = new DateTime(year, month, day);

                Console.WriteLine("Enter Rent End! ");
                Console.Write("Year:");
                int yeare = int.Parse(Console.ReadLine());
                Console.Write("Month:");
                int monthe = int.Parse(Console.ReadLine());
                Console.Write("Day:");
                int daye = int.Parse(Console.ReadLine());
                one.End = new DateTime(yeare, monthe, daye);
                Console.WriteLine("Enter Carid");
                List("Car");
                one.CarId = int.Parse(Console.ReadLine());
                rest.Put(one, "rent");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Brand")
            {
                List("Brand");
                Console.Write("Enter Brand's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "brand");
            }
            else if (entity == "Rent")
            {
                List("Rent");
                Console.Write("Enter Rent's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "rent");
                
            }
            else if (entity=="Car")
            {
                List("Car");
                Console.Write("Enter Car's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "rent");
            }
        }
        static void SPBB()
        {
            rest.
        }


        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:10237/", "brand");

            var brandSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Brand"))
                .Add("Create", () => Create("Brand"))
                .Add("Delete", () => Delete("Brand"))
                //.Add("Update", () => Update("Brand"))
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

            var nonCruds = new ConsoleMenu(args, level: 1)
                .Add("SumPriceByBrand", () => SPBB())
                .Add("FreqOfBrandsRented", () => SPBB())
                .Add("AvgPriceByBrand", () => SPBB())
                .Add("FreqOfCarsRented", () => SPBB())
                .Add("RentTimes", () => SPBB())
                .Add("Exit", ConsoleMenu.Close);




            var menu = new ConsoleMenu(args, level: 0)
                .Add("Car", () => carSubMenu.Show())
                .Add("Brands", () => brandSubMenu.Show())
                .Add("Rent", () => rentSubMenu.Show())
                .Add("Non-Cruds",()=>nonCruds.Show())
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
