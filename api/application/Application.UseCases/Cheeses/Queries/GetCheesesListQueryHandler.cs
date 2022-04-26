using Application.Core;
using Domain.Data.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Cheeses.Queries
{
    public class GetCheesesListQueryHandler : IUseCaseHandler<GetCheesesListQuery, IList<CheeseViewModel>>
    {
        private readonly ILogger<GetCheesesListQueryHandler> logger;
        private readonly ICheeseRepository cheeseRepository;

        public GetCheesesListQueryHandler(ILogger<GetCheesesListQueryHandler> logger, ICheeseRepository cheeseRepository)
        {
            this.logger=logger;
            this.cheeseRepository=cheeseRepository;
        }

        public async Task<IList<CheeseViewModel>> HandleAsync(GetCheesesListQuery request, CancellationToken cancellationToken)
        {
            var cheeses = this.cheeseRepository.GetCheeses();

            if (cheeses == null)
                return new List<CheeseViewModel>();

            return cheeses.Select(c => new CheeseViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Color= c.Color,
                PricePerKg = c.PricePerKg/100m,
                Image = c.Image
            }).ToList();
        }
    }
}
