using Domain.Data.Extensions;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Data.Repositories
{
    //I used a memory cache for persistence. I thought about using an in memory EF SQL database or Redis cache. 
    public class CheeseRepository : ICheeseRepository
    {
        private const string CheeseKey = "cheeseKey";
        private readonly ILogger<CheeseRepository> logger;
        private readonly IMemoryCache memoryCache;

        private MemoryCacheEntryOptions CacheOptions => new MemoryCacheEntryOptions();

        public CheeseRepository(ILogger<CheeseRepository> logger, IMemoryCache memoryCache)
        {
            this.logger=logger;
            this.memoryCache=memoryCache;
        }

        public Cheese GetCheeseByID(Guid cheeseId)
        {
            try
            {
                var allCheeses = GetCheeseList();
                var cheese = allCheeses.FirstOrDefault(i => i.Id == cheeseId);
                return cheese;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Error when retrieving cheese by ID");
                throw;
            }
        }

        public IEnumerable<Cheese> GetCheeses()
        {
            try
            {
                if (this.memoryCache.TryGetValue(CheeseKey, out IEnumerable<Cheese> cheeseCollection))
                {
                    return cheeseCollection;
                }

                return cheeseCollection;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Error when retrieving list of cheeses");
                throw;
            }
        }

        public Guid Insert(Cheese cheese)
        {
            try
            {
                var allCheeses = GetCheeseList();
                cheese.Id = Guid.NewGuid();
                allCheeses.Add(cheese);

                this.memoryCache.Set(CheeseKey, allCheeses, CacheOptions);
                return cheese.Id;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Error when attempting to insert cheese");
                throw;
            }
        }

        public void Update(Cheese cheese)
        {
            try
            {
                var allCheeses = GetCheeseList();
                var cheeseToUpdate = allCheeses.FirstOrDefault(i => i.Id == cheese.Id);

                cheeseToUpdate.Name = cheese.Name;
                cheeseToUpdate.Color = cheese.Color;
                cheeseToUpdate.Image = cheeseToUpdate.Image;
                if ((cheeseToUpdate?.Image?.Length??0) > 0)
                {
                    cheeseToUpdate.Image = cheese.Image;
                }

                this.memoryCache.Set(CheeseKey, allCheeses, CacheOptions);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Error when attempting to update cheese");
                throw;
            }
        }

        public void Delete(Guid cheeseId)
        {
            try
            {
                var allCheeses = GetCheeseList();
                var cheeseToUpdate = allCheeses.FirstOrDefault(i => i.Id == cheeseId);
                allCheeses.Remove(cheeseToUpdate);
                this.memoryCache.Set(CheeseKey, allCheeses, CacheOptions);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Error when attempting to update cheese");
                throw;
            }
        }

        private List<Cheese> GetCheeseList()
        {
            var cheeses = GetCheeses();
            return cheeses?.ToList() ?? new List<Cheese>();
        }
    }
}
