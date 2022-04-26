using System.Threading;
using System.Threading.Tasks;

namespace Application.Core
{
    public interface IUseCaseHandler<TRequest, TResponse> where TRequest : IUseCaseRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
