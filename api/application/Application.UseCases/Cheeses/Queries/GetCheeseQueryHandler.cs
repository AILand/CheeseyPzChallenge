using Application.Core;
using Domain.Data.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Cheeses.Queries
{
    public class GetCheeseQueryHandler : IUseCaseHandler<GetCheeseQuery, CheeseViewModel>
    {
        private readonly ILogger<GetCheeseQueryHandler> logger;
        private readonly ICheeseRepository cheeseRepository;

        public GetCheeseQueryHandler(ILogger<GetCheeseQueryHandler> logger, ICheeseRepository cheeseRepository)
        {
            this.logger=logger;
            this.cheeseRepository=cheeseRepository;
        }

        public async Task<CheeseViewModel> HandleAsync(GetCheeseQuery request, CancellationToken cancellationToken)
        {
            var cheeses = this.cheeseRepository.GetCheeses();
            var c = cheeses.SingleOrDefault(i => i.Id == request.Id);

            if (c != null)
            {
                return new CheeseViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Color= c.Color,
                    PricePerKg = c.PricePerKg/100m,
                    Image = c.Image
                };
            }

            return null;
        }
    }
}
