﻿using System;
using System.Collections.Generic;
using System.Linq;
using YL1CC3_HFT_2022231.Models;
using YL1CC3_HFT_2022231.Repository;

namespace YL1CC3_HFT_2022231.Logic
{
    public class BrandLogic : IBrandLogic
    {
        IRepository<Brand> repo;
        public IEnumerable<PriceOfBrands> SumPriceByBrand()
        {
            return (from x in repo.ReadAll()
                        select new PriceOfBrands()
                        {
                            Brand = x.Name,
                            Price=x.Cars.Select(t=>t.Price).Sum()
                        }).OrderBy(x=>x.Price);
        }
        public BrandLogic(IRepository<Brand> repo)
        {
            this.repo = repo;
        }

        public void Create(Brand item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Brand Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Brand> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Brand item)
        {
            this.repo.Update(item);
        }
    }

    public class PriceOfBrands
    {
        public string Brand { get; set; }
        public int Price { get; set; }
    }
}