using System;
using System.Linq;
using YL1CC3_HFT_2022231.Models;
using YL1CC3_HFT_2022231.Repository;

namespace YL1CC3_HFT_2022231.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CarDbContext db = new CarDbContext();
            var asd = from x in db.Rents
                      group x by x.CarId into g
                      select new
                      {
                          asd = g.Key,
                      };

            var nemar = new CarRepository(db).ReadAll().Where(t => t.Rents.FirstOrDefault() != null);
            var res1 = from m in db.Brands
                       group m by m.Id into g
                       select new
                       {
                           
                           Auto=db.Cars,
                       };
            var nemertem = from x in db.Cars
                           group x by x.BrandId into g
                           select new
                           {
                               Brand = g.Key,
                               Model=g.FirstOrDefault(),
                           };

           // var alma= db.Cars.Where(t=>t.Model.FirstOrDefault)
            ;
        }
    }
}
