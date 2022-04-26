using Domain.Entities;
using System;
using System.Text;

namespace Application.Core.UnitTests.Helpers
{
    internal class TestHelper
    {
        internal  static Cheese BuildCheese(string name, string color, int pricePerKg, string imageDataStr = "imagedata")
        {
            var byteArray = Encoding.UTF8.GetBytes(imageDataStr);
            var cheese = new Cheese()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Color = color,
                PricePerKg = pricePerKg,
                Image = byteArray
            };

            return cheese;
        }
    }
}
