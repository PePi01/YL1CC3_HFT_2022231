using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YL1CC3_HFT_2022231.Models;
using YL1CC3_HFT_2022231.Repository;

namespace YL1CC3_HFT_2022231.Logic
{
    public class RentLogic : IRentLogic
    {
        IRepository<Rent> repo;
        public static IEnumerable<RentFrequency> FreqOfRents()
        {
            CarDbContext db = new CarDbContext();
            var cars = new CarRepository(db);
            var rents = new RentRepository(db);
            var brands = new BrandRepository(db);

            var carLogic = new CarLogic(cars);
            var rentLogic = new RentLogic(rents);
            var brandLogic = new BrandLogic(brands);

            return    from x in db.Rents.AsEnumerable()
                      group x by x.CarId into g
                      select new RentFrequency
                      {
                          Frequency = g.Count(),
                          Model = g.Select(t => t.Car.Model),
                      };

        }
        public RentLogic(IRepository<Rent> repo)
        {
            this.repo = repo;
        }

        public void Create(Rent item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Rent Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Rent> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Rent item)
        {
            this.repo.Update(item);
        }
    }

    public class RentFrequency
    {
        public int Frequency { get; set; }
        public IEnumerable<string> Model { get; set; }
    }
}
