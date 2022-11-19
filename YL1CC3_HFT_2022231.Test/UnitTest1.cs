using Moq;
using NUnit.Framework;
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
            CarLogic logic;
            Mock<IRepository<Car>> mockCarRepo;

            [SetUp]
            public void Init()
            {
                mockCarRepo = new Mock<IRepository<Car>>();
                mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>()
            {
                new Car() { Id = 1, BrandId = 1, Price = 20000, Model = "BMW 116d" },
                new Car() { Id = 5, BrandId = 3, Price = 20000, Model = "Audi A3" },
                new Car() { Id = 6, BrandId = 3, Price = 25000, Model = "Audi A4" },
                new Car() { Id = 7, BrandId = 4, Price = 25000, Model = "VW Golf 4" },
                new Car() { Id = 8, BrandId = 4, Price = 33000, Model = "VW Golf 5" },
            }.AsQueryable());
                logic = new CarLogic(mockCarRepo.Object);
            }

            [Test]
            public void AvgRatePerYearTest()
            {
                var avg = logic.FreqOfCarsRented();
                //Assert.That(avg.));
            }

            [Test]
            public void YearStatisticsTest()
            {
                var actual = logic.YearStatistics().ToList();
                var expected = new List<YearInfo>()
            {
                new YearInfo()
                {
                    Year = 2008,
                    AvgRating = 5,
                    MovieNumber = 1
                },
                new YearInfo()
                {
                    Year = 2009,
                    AvgRating = 6.5,
                    MovieNumber = 2
                },
                new YearInfo()
                {
                    Year = 2010,
                    AvgRating = 8,
                    MovieNumber = 1
                }
            };

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void CreateMovieTestWithCorrectTitle()
            {
                var movie = new Movie() { Title = "Vukk" };

                //ACT
                logic.Create(movie);

                //ASSERT
                mockMovieRepo.Verify(r => r.Create(movie), Times.Once);
            }

            [Test]
            public void CreateMovieTestWithInCorrectTitle()
            {
                var movie = new Movie() { Title = "24" };
                try
                {
                    //ACT
                    logic.Create(movie);
                }
                catch
                {

                }

                //ASSERT
                mockMovieRepo.Verify(r => r.Create(movie), Times.Never);
            }
        }
    
}