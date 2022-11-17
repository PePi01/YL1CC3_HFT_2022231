﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YL1CC3_HFT_2022231.Models;

namespace YL1CC3_HFT_2022231.Repository
{
    class RentRepository : Repository<Rent>, IRepository<Rent>
    {
        public RentRepository(CarDbContext ctx) : base(ctx)
        {
        }

        public override Rent Read(int id)
        {
            return ctx.Rents.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Rent item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
