using Application.Core.Models;
using System;

namespace Application.UseCases.Cheeses.Queries
{
    public class CheeseViewModel : ICheeseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal PricePerKg { get; set; }
        public byte[]? Image { get; set; }
    }
}
