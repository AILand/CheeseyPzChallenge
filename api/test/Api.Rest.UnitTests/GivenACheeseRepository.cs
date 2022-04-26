using Application.Core.UnitTests.Helpers;
using Domain.Data.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Api.Rest.UnitTests
{
    public class GivenACheeseRepository
    {
        private const string CheeseKey = "cheeseKey";

        [Fact]
        public void WhenAddingANewCheese_ThenCheeseShouldBeFound()
        {
            //Arrange
            var myLoggerFactory = new NullLoggerFactory();
            var myLogger = myLoggerFactory.CreateLogger<CheeseRepository>();
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cheeseRepository = new CheeseRepository(myLogger, memoryCache);
            var cheese = TestHelper.BuildCheese("Gouda", "red", 350, "imagedata");

            //Act
            cheeseRepository.Insert(cheese);

            //Assert
            bool found = memoryCache.TryGetValue(CheeseKey, out IEnumerable<Cheese> cheeseCollection);
            Assert.True(found == true, $"Key: '{CheeseKey}' could not be found in memory cache");
            Assert.NotEmpty(cheeseCollection);
            Assert.Single(cheeseCollection.ToList());
            Assert.Equal(cheeseCollection.First(), cheese);
        }

        [Fact]
        public void WhenDeletingACheese_ThenCheeseShouldNotBeFound()
        {
            //Arrange
            var myLoggerFactory = new NullLoggerFactory();
            var myLogger = myLoggerFactory.CreateLogger<CheeseRepository>();
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cheeseRepository = new CheeseRepository(myLogger, memoryCache);
            var cheese1 = TestHelper.BuildCheese("Gouda", "red", 350, "imagedata");
            var cheese2 = TestHelper.BuildCheese("Cheddar", "yellow", 430, "imagedata");
            var allCheeses = new List<Cheese>()
            {
                cheese1,
                cheese2,
            };
            memoryCache.Set(CheeseKey, allCheeses);
            memoryCache.TryGetValue(CheeseKey, out IEnumerable<Cheese> existingCheeseCollection);
            var cachedCheese = existingCheeseCollection.FirstOrDefault(i => i.Id == cheese1.Id);
            var cheeseExisted = cheese1.Id == cachedCheese.Id;

            //Act
            cheeseRepository.Delete(cachedCheese.Id);

            //Assert
            bool found = memoryCache.TryGetValue(CheeseKey, out IEnumerable<Cheese> cheeseCollection);
            Assert.True(cheeseExisted, "Cheese did not exist to be deleted");
            Assert.True(found == true, $"Key: '{CheeseKey}' could not be found in memory cache");
            Assert.Single(cheeseCollection.ToList());
            Assert.True(cheeseCollection.ToList().FirstOrDefault()?.Id != cheese1.Id);
            Assert.True(cheeseCollection.ToList().FirstOrDefault()?.Id == cheese2.Id);
        }

        [Fact]
        public void WhenUpdatingACheese_ThenCheeseDetailsShouldBeChanged()
        {
            //Arrange
            var myLoggerFactory = new NullLoggerFactory();
            var myLogger = myLoggerFactory.CreateLogger<CheeseRepository>();
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cheeseRepository = new CheeseRepository(myLogger, memoryCache);
            var cheese = TestHelper.BuildCheese("Gouda", "red", 350, "imagedata");
            var allCheeses = new List<Cheese>()
            {
                cheese
            };
            memoryCache.Set(CheeseKey, allCheeses);
            memoryCache.TryGetValue(CheeseKey, out IEnumerable<Cheese> existingCheeseCollection);
            var cachedCheese = existingCheeseCollection.First();
            var cheeseExisted = cheese.Id == cachedCheese.Id && cheese.Color == cachedCheese.Color;

            //Act
            cheese.Color = "green";
            cheeseRepository.Update(cheese);

            //Assert
            bool found = memoryCache.TryGetValue(CheeseKey, out IEnumerable<Cheese> cheeseCollection);
            var updatedCheese = cheeseCollection.FirstOrDefault();
            Assert.True(cheeseExisted, "Cheese did not exist to be deleted");
            Assert.True(found == true, $"Key: '{CheeseKey}' could not be found in memory cache");
            Assert.True(updatedCheese.Color == "green");
        }
    }
}
