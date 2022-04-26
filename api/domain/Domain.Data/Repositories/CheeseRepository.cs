using Domain.Data.Extensions;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Data.Repositories
{
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
            var allCheeses = GetCheeseList();
            var cheese = allCheeses.FirstOrDefault(i => i.Id == cheeseId);
            return cheese;
        }

        public IEnumerable<Cheese> GetCheeses()
        {
            if (this.memoryCache.TryGetValue(CheeseKey, out IEnumerable<Cheese> cheeseCollection))
            {
                return cheeseCollection;
            }

            return cheeseCollection;
        }

        public Guid Insert(Cheese cheese)
        {
            var allCheeses = GetCheeseList();
            cheese.Id = Guid.NewGuid();
            allCheeses.Add(cheese);

            this.memoryCache.Set(CheeseKey, allCheeses, CacheOptions);
            return cheese.Id;
        }

        public void Update(Cheese cheese)
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

        public void Delete(Guid cheeseId)
        {
            var allCheeses = GetCheeseList();
            var cheeseToUpdate = allCheeses.FirstOrDefault(i => i.Id == cheeseId);
            allCheeses.Remove(cheeseToUpdate);
            this.memoryCache.Set(CheeseKey, allCheeses, CacheOptions);
        }

        private List<Cheese> GetCheeseList()
        {
            //var allCheeses = new List<Cheese>();
            var cheeses = GetCheeses();
            //if (cheeses != null)
            //    cheeses.ForEach(i => allCheeses.Add(i));

            return cheeses?.ToList() ?? new List<Cheese>();
            //== null ? new List<Cheese>():  cheeses?.ToList();

            //return allCheeses;
        }
    }
}
