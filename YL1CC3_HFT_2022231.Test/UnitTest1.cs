using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using YL1CC3_HFT_2022231.Logic;
using YL1CC3_HFT_2022231.Models;
using YL1CC3_HFT_2022231.Repository;

namespace YL1CC3_HFT_2022231.Test
{

    [TestFixture]
    public class CarLogicTester
    {
        CarLogic logicCar;
        RentLogic logicRent;
        BrandLogic logicBrand;
        Mock<IRepository<Car>> mockCarRepo;
        Mock<IRepository<Rent>> mockRentRepo;
        Mock<IRepository<Brand>> mockBrandRepo;

        //[SetUp]
        //public void Init()
        //{
        //    mockCarRepo = new Mock<IRepository<Car>>();
        //    mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>()
        //        {
        //            new Car() { Id = 1, BrandId = 1, Price = 20000, Model = "BMW 116d"
        //            ,Rents=new List<Rent>(){ new Rent() { Id = 1, CarId = 1, Start = new DateTime(2020, 10, 15), End = new DateTime(2020, 10, 16) } } },
        //            new Car() { Id = 2, BrandId = 3, Price = 20000, Model = "Audi A3"
        //            ,Rents=new List<Rent>(){ new Rent() { Id = 2, CarId = 2, Start = new DateTime(2019, 8, 27), End = new DateTime(2020, 10, 1) }
        //            ,new Rent() {  Id = 4, CarId = 2, Start = new DateTime(2018, 1, 1), End = new DateTime(2019, 1, 16)} } },

        //        }.AsQueryable());
        //    logicCar = new CarLogic(mockCarRepo.Object);

        //    mockRentRepo = new Mock<IRepository<Rent>>();
        //    mockRentRepo.Setup(m => m.ReadAll()).Returns(new List<Rent>()
        //        {
        //            new() { Id = 1, CarId = 1, Start = new DateTime(2020, 10, 15), End = new DateTime(2020, 10, 16) },
        //            new() { Id = 2, CarId = 2, Start = new DateTime(2019, 8, 27), End = new DateTime(2020, 10, 1) },
        //            new() { Id = 3, CarId = 3, Start = new DateTime(2022, 7, 8), End = new DateTime(2022, 9, 13) },
        //            new() { Id = 4, CarId = 4, Start = new DateTime(2018, 1, 1), End = new DateTime(2019, 1, 16) },
        //            new() { Id = 5, CarId = 4, Start = new DateTime(2020, 10, 14), End = new DateTime(2020, 10, 18) },
        //            new() { Id = 6, CarId = 3, Start = new DateTime(2019, 7, 20), End = new DateTime(2020, 1, 16) },
        //        }.AsQueryable());
        //    logicRent = new RentLogic(mockRentRepo.Object);

        //    mockBrandRepo = new Mock<IRepository<Brand>>();
        //    mockBrandRepo.Setup(t => t.ReadAll()).Returns(new List<Brand>()
        //            {
        //            new Brand() { Id = 1, Name = "BMW" },
        //            new Brand() { Id = 2, Name = "Citroen" },
        //            new Brand() { Id = 3, Name = "Audi" },
        //            new Brand() { Id = 4, Name = "VW" },
        //            new Brand() { Id = 5, Name = "Renault" }
        //            }.AsQueryable());
        //    logicBrand = new BrandLogic(mockBrandRepo.Object);
        //}

        [Test]
        public void FreqOfCarsRentedTest()
        {

            var malac = new List<Car>()
                {
                    new Car() { Id = 1, BrandId = 1, Price = 20000, Model = "BMW 116d"
                    ,Rents=new List<Rent>(){ new Rent() { Id = 1, CarId = 1, Start = new DateTime(2020, 10, 15), End = new DateTime(2020, 10, 16) } } },
                    new Car() { Id = 2, BrandId = 3, Price = 20000, Model = "Audi A3"
                    ,Rents=new List<Rent>(){ new Rent() { Id = 2, CarId = 2, Start = new DateTime(2019, 8, 27), End = new DateTime(2020, 10, 1) }
                    ,new Rent() {  Id = 4, CarId = 2, Start = new DateTime(2018, 1, 1), End = new DateTime(2019, 1, 16)} } },

                }.AsQueryable();
            mockCarRepo = new Mock<IRepository<Car>>();
            mockCarRepo.Setup(m => m.ReadAll()).Returns(malac);
            logicCar = new CarLogic(mockCarRepo.Object);


            var avg = logicCar.FreqOfCarsRented();

            Assert.That(avg.ElementAt(0).Frequency == 1);
            Assert.That(avg.ElementAt(1).Frequency == 2);
            Assert.That(avg.ElementAt(0).Model == "BMW 116d");
            Assert.That(avg.ElementAt(1).Model == "Audi A3");
        }

        [Test]
        public void SumPriceByBrandTest()
        {
            mockBrandRepo = new Mock<IRepository<Brand>>();
            mockBrandRepo.Setup(t => t.ReadAll()).Returns(new List<Brand>()
                        {
                        new Brand() { Id = 1, Name = "BMW",Cars=new List<Car>(){ new Car() { Id = 1, BrandId = 1, Price = 30000, Model = "BMW 116d" } } },
                        new Brand() { Id = 2, Name = "Citroen", Cars=new List<Car>(){ new Car() { Id = 2, BrandId = 2, Price = 20000, Model = "Citroen c3" },new Car() { Id = 3, BrandId = 2, Price = 25000, Model = "Citroen c4" } } },

                        }.AsQueryable());
            logicBrand = new BrandLogic(mockBrandRepo.Object);
            var test = logicBrand.SumPriceByBrand();
            Assert.That(test.ElementAt(0).Price == 30000);
            Assert.That(test.ElementAt(1).Price == 45000);
            Assert.That(test.ElementAt(0).Brand == "BMW");
            Assert.That(test.ElementAt(1).Brand == "Citroen");
        }

        [Test]
        public void FreqOfBrandsRentedTest()
        {
            mockBrandRepo = new Mock<IRepository<Brand>>();
            var repo = new List<Brand>()
                        {
                        new Brand() { Id = 1, Name = "BMW",Cars=new List<Car>(){ new Car() { Id = 1, BrandId = 1, Price = 30000, Model = "BMW 116d",Brand=new Brand() { Id = 1, Name = "BMW"  },Rents=new List<Rent>() { new Rent() { Id = 1, CarId = 1, Start = new DateTime(2020, 10, 15), End = new DateTime(2020, 10, 16) } } } } },
                        new Brand() { Id = 2, Name = "Citroen", Cars=new List<Car>(){ new Car() { Id = 2, BrandId = 2, Price = 20000, Model = "Citroen c3", Brand = new Brand() { Id = 2, Name = "Citroen" }, Rents=new List<Rent>() { new Rent() { Id = 2, CarId = 2, Start = new DateTime(2019, 10, 15), End = new DateTime(2020, 5, 16) } } },new Car() { Id = 3, BrandId = 2, Price = 25000, Model = "Citroen c4", Brand = new Brand() { Id = 2, Name = "Citroen" }, Rents=new List<Rent>() {  new Rent() { Id = 3, CarId = 3, Start = new DateTime(2021, 10, 5), End = new DateTime(2022, 10, 1) } } } } },
                        }.AsQueryable();
            logicBrand = new BrandLogic(mockBrandRepo.Object);
            mockBrandRepo.Setup(t => t.ReadAll()).Returns(repo);
            var test1 = logicBrand.FreqOfBrandsRented();
            Assert.That(test1.ElementAt(0).Frequency == 1);
            Assert.That(test1.ElementAt(0).Brand == "BMW");
            Assert.That(test1.ElementAt(1).Frequency == 2);
            Assert.That(test1.ElementAt(1).Brand == "Citroen");
        }
        [Test]
        public void AvgPriceByBrandTest()
        {
            mockBrandRepo = new Mock<IRepository<Brand>>();
            var repo = new List<Brand>()
                        {
                        new Brand() { Id = 1, Name = "BMW",Cars=new List<Car>(){ new Car() { Id = 1, BrandId = 1, Price = 30000, Model = "BMW 116d",Brand=new Brand() { Id = 1, Name = "BMW"  },Rents=new List<Rent>() { new Rent() { Id = 1, CarId = 1, Start = new DateTime(2020, 10, 15), End = new DateTime(2020, 10, 16) } } } } },
                        new Brand() { Id = 2, Name = "Citroen", Cars=new List<Car>(){ new Car() { Id = 2, BrandId = 2, Price = 20000, Model = "Citroen c3", Brand = new Brand() { Id = 2, Name = "Citroen" }, Rents=new List<Rent>() { new Rent() { Id = 2, CarId = 2, Start = new DateTime(2019, 10, 15), End = new DateTime(2020, 5, 16) } } },new Car() { Id = 3, BrandId = 2, Price = 25000, Model = "Citroen c4", Brand = new Brand() { Id = 2, Name = "Citroen" }, Rents=new List<Rent>() {  new Rent() { Id = 3, CarId = 3, Start = new DateTime(2021, 10, 5), End = new DateTime(2022, 10, 1) } } } } },
                        }.AsQueryable();
            logicBrand = new BrandLogic(mockBrandRepo.Object);
            mockBrandRepo.Setup(t => t.ReadAll()).Returns(repo);
            var test1 = logicBrand.AvgPriceByBrand();
            Assert.That(test1.ElementAt(0).Price == 22500);
            Assert.That(test1.ElementAt(0).Brand == "Citroen");
            Assert.That(test1.ElementAt(1).Price == 30000);
            Assert.That(test1.ElementAt(1).Brand == "BMW");
        }
        [Test]
        public void RentTimesTest()
        {
            mockRentRepo = new Mock<IRepository<Rent>>();
            var rent = new List<Rent>()
                    {
                        new() {Car=new Car(){ Model="bmw 116d"}, Id = 1, CarId = 1, Start = new DateTime(2020, 10, 15), End = new DateTime(2020, 10, 16) },
                        new() { Car=new Car(){ Model="bmw e39"},Id = 2, CarId = 2, Start = new DateTime(2019, 8, 27), End = new DateTime(2020, 10, 1) },
                    }.AsQueryable();
            logicRent = new RentLogic(mockRentRepo.Object);
            mockRentRepo.Setup(m => m.ReadAll()).Returns(rent);
            var test = logicRent.RentTimes();
            Assert.That(test.ElementAt(0).Days == 1);
            Assert.That(test.ElementAt(0).Model == "bmw 116d");
            Assert.That(test.ElementAt(1).Days == 401);
            Assert.That(test.ElementAt(1).Model == "bmw e39");
        }
        [Test]
        public void CarLogicCreateTest()
        {
            mockCarRepo = new Mock<IRepository<Car>>();
            var asd = new List<Car>() { new Car() { Price = -500 } }.AsQueryable();
            mockCarRepo.Setup(t => t.ReadAll()).Returns(asd);
            logicCar = new CarLogic(mockCarRepo.Object);
            Assert.That(() => logicCar.Create(asd.ElementAt(0)), Throws.Exception);
        }
        [Test]
        public void BrandLogicCreateTest()
        {
            mockBrandRepo = new Mock<IRepository<Brand>>();
            var asd=new List<Brand>() { new Brand() { Name="aaaaaaaaaaaaaaaa"} }.AsQueryable();
            mockBrandRepo.Setup(t => t.ReadAll()).Returns(asd);
            logicBrand = new BrandLogic(mockBrandRepo.Object);
            Assert.That(() => logicBrand.Create(asd.ElementAt(0)), Throws.Exception);
        }
        [Test]
        public void RentLogicReadTest()
        {
            mockRentRepo = new Mock<IRepository<Rent>>();
            var asd=new List<Rent>() { new Rent() { Id=0} }.AsQueryable();
            mockRentRepo.Setup(t => t.ReadAll()).Returns(asd);
            logicRent = new RentLogic(mockRentRepo.Object);
            Assert.That(() => logicRent.Read(0), Throws.Exception);
        }
        [Test]
        public void RentLogicDeleteTest()
        {
            mockRentRepo = new Mock<IRepository<Rent>>();
            var asd = new List<Rent>() { new Rent() { Id = 0 } }.AsQueryable();
            mockRentRepo.Setup(t => t.ReadAll()).Returns(asd);
            logicRent = new RentLogic(mockRentRepo.Object);
            Assert.That(() => logicRent.Delete(1), Throws.Nothing);
        }
    }

}