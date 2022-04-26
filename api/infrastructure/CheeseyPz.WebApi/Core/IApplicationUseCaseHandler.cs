using Application.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CheeseyPz.WebApi.Core
{
    public interface IApplicationUseCaseHandler
    {
        Task<TResponse> HandleAsync<TResponse>(IUseCaseRequest<TResponse> message, CancellationToken cancellationToken);
    }
}
