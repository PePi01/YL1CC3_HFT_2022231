using System;
using System.Linq;
using YL1CC3_HFT_2022231.Logic;
using YL1CC3_HFT_2022231.Models;
using YL1CC3_HFT_2022231.Repository;

namespace YL1CC3_HFT_2022231.Client
{
    class Program
    {
        static void Main(string[] args)
        {


            CarDbContext db = new CarDbContext();
            var cars = new CarRepository(db);
            var rents = new RentRepository(db);
            var brands = new BrandRepository(db);

            var carLogic = new CarLogic(cars);
            var rentLogic = new RentLogic(rents);
            var brandLogic = new BrandLogic(brands);


            var asd = from x in db.Rents.AsEnumerable()
                      group x by x.CarId into g
                      select new
                      {
                          Frequency = g.Count(),
                          Model = g.Select(t => t.Car.Model),
                      };
            var asd2 = from x in new RentRepository(db).ReadAll().AsEnumerable()
                       group x by x.CarId into g
                       select new
                       {
                           Frequency = g.Count(),
                           Model = g.Select(t => t.Car.Model),
                       };

            var nemar = new CarRepository(db).ReadAll().Where(t => t.Rents.FirstOrDefault() != null);
            var res1 = from m in db.Brands
                       group m by m.Id into g
                       select new
                       {
                           
                           Auto=db.Cars,
                       };

            var carrrr = db.Cars.ToList();
            var nem = from x in db.Cars
                      select x;
            var idk = from x in db.Cars.AsEnumerable()
                      group x by x.BrandId into g
                      select new
                      {
                          Brand=g.Select(t=>t.Brand.Name),
                          Model = g.Select(t => t.Model),
                      };
            //crud1
            var price = from x in new CarRepository(db).ReadAll().AsEnumerable()
                        group x by x.Brand into g
                        select new
                        {
                            Brand = g.Key.Name,
                            Sum = g.Sum(t => t.Price)
                        };
            var seg=from x in rents.ReadAll().AsEnumerable()
            group x by x.CarId into g
            select new 
            {
                Frequency = g.Count(),
                Model = g.Select(t => t.Car.Model),
            };
            // NON CRUDOK 
            var segg = carLogic.FreqOfCarsRented();
            var fos = brandLogic.SumPriceByBrand();

            ;
        }
    }
}
