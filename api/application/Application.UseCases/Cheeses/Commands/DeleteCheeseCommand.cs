using Application.Core;
using System;

namespace Application.UseCases.Cheeses.Commands
{
    public class DeleteCheeseCommand : IUseCaseRequest<NoContent>
    {
        public Guid Id { get; set; }
    }
}
