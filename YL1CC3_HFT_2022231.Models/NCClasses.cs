using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YL1CC3_HFT_2022231.Models
{
    public class PriceOfBrands
    {
        public string Brand { get; set; }
        public int Price { get; set; }
    }
    public class AvgPriceOfBrands
    {
        public string Brand { get; set; }
        public double Price { get; set; }
    }

    public class RentBrandFrequency
    {
        public string Brand { get; set; }
        public int Frequency { get; set; }
    }
}
