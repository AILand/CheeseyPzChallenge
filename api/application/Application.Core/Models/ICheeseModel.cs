using System;

namespace Application.Core.Models
{
    public interface ICheeseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal PricePerKg { get; set; }
        public byte[] Image { get; set; }
    }
}
