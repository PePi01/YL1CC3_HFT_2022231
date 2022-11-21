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

                var malac=new List<Car>()
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

            int[] freq = { 1, 2 };
            string[] models = { "BMW 116d ", "Audi A3" };
            ;
            var avg = logicCar.FreqOfCarsRented();
            for (int i = 0; i < avg.Count(); i++)
            {
                Assert.That(avg.ElementAt(i).Frequency == freq[i] && avg.ElementAt(i).Model == models[i]);
            }
        }

        //[Test]
        //public void YearStatisticsTest()
        //{
        //    var actual = logic.YearStatistics().ToList();
        //    var expected = new List<YearInfo>()
        //    {
        //        new YearInfo()
        //        {
        //            Year = 2008,
        //            AvgRating = 5,
        //            MovieNumber = 1
        //        },
        //        new YearInfo()
        //        {
        //            Year = 2009,
        //            AvgRating = 6.5,
        //            MovieNumber = 2
        //        },
        //        new YearInfo()
        //        {
        //            Year = 2010,
        //            AvgRating = 8,
        //            MovieNumber = 1
        //        }
        //    };

        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void CreateMovieTestWithCorrectTitle()
        //{
        //    var movie = new Movie() { Title = "Vukk" };

        //    //ACT
        //    logic.Create(movie);

        //    //ASSERT
        //    mockMovieRepo.Verify(r => r.Create(movie), Times.Once);
        //}

        //[Test]
        //public void CreateMovieTestWithInCorrectTitle()
        //{
        //    var movie = new Movie() { Title = "24" };
        //    try
        //    {
        //        //ACT
        //        logic.Create(movie);
        //    }
        //    catch
        //    {

        //    }

        //    //ASSERT
        //    mockMovieRepo.Verify(r => r.Create(movie), Times.Never);
        //}
    }

}