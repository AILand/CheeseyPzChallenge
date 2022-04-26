using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Data.Repositories
{
    public interface ICheeseRepository
    {
        IEnumerable<Cheese> GetCheeses();
        Cheese GetCheeseByID(Guid cheeseId);
        Guid Insert(Cheese cheese);
        void Delete(Guid cheeseID);
        void Update(Cheese cheese);
    }
}
