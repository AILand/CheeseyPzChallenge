using Application.Core;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.UseCases.Cheeses.Commands
{
    public class UpsertCheeseCommand : IUseCaseRequest<Guid>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal PricePerKg { get; set; }
        public IFormFile Image { get; set; }
    }
}
