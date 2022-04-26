using Application.Core;
using System;

namespace Application.UseCases.Cheeses.Queries
{
    public class GetCheeseQuery : IUseCaseRequest<CheeseViewModel>
    {
        public Guid Id { get; set; }
    }
}
