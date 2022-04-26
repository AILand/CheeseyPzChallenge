using Application.Core;
using Domain.Data.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Cheeses.Commands
{
    public class DeleteCheeseCommandHandler : IUseCaseHandler<DeleteCheeseCommand, NoContent>
    {
        private readonly ILogger<DeleteCheeseCommandHandler> logger;
        private readonly ICheeseRepository cheeseRepository;

        public DeleteCheeseCommandHandler(ILogger<DeleteCheeseCommandHandler> logger, ICheeseRepository cheeseRepository)
        {
            this.logger=logger;
            this.cheeseRepository=cheeseRepository;
        }

        public async Task<NoContent> HandleAsync(DeleteCheeseCommand request, CancellationToken cancellationToken)
        {
            return await Delete(request, cancellationToken);
        }

        private async Task<NoContent> Delete(DeleteCheeseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                this.cheeseRepository.Delete(request.Id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error deleting Cheese: {request.Id}", ex);
                throw;
            }
            return null;
        }


    }
}

