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
                      group x by x.Interval into g
                      select new
                      {
                          asd = g.Key,
                      };
            ;
        }
    }
}
