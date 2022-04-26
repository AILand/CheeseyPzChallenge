using Application.Core;
using Application.Core.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace CheeseyPz.WebApi.Core
{
	public delegate object ServiceFactory(Type serviceType);

	public class ApplicationUseCaseHandler : IApplicationUseCaseHandler
	{
		private readonly ServiceFactory serviceFactory;

		public ApplicationUseCaseHandler(ServiceFactory serviceFactory)
		{
			this.serviceFactory = serviceFactory;
		}

		public Task<TResponse> HandleAsync<TResponse>(IUseCaseRequest<TResponse> request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new BadRequestException("Request was null");
			}

			var requestType = request.GetType();
			var handler = (dynamic)Activator.CreateInstance(typeof(RequestWrapper<,>).MakeGenericType(requestType, typeof(TResponse)));

			return handler.HandleAsync(serviceFactory, request, cancellationToken);
		}

		private class RequestWrapper<TRequest, TResponse> where TRequest : IUseCaseRequest<TResponse>
		{
			internal Task<TResponse> HandleAsync(ServiceFactory serviceFactory, object request, CancellationToken cancellationToken)
			{
				var requestType = request.GetType();
				var validatorType = typeof(IValidator<>).MakeGenericType(requestType);
				var validatorEnumerableType = typeof(IEnumerable<>).MakeGenericType(validatorType);
				var validators = serviceFactory(validatorEnumerableType) as IEnumerable<IValidator<TRequest>>;
				Validate(request, validators);

				var t = typeof(IUseCaseHandler<,>).MakeGenericType(requestType, typeof(TResponse));
				var handler = serviceFactory(t);

				if (handler == null)
				{
					throw new Exception("No handler was found to service the request");
				}

				return (handler as IUseCaseHandler<TRequest, TResponse>).HandleAsync((TRequest)request, cancellationToken);
			}

			private void Validate(object request, IEnumerable<IValidator<TRequest>> validators)
			{
				var context = new ValidationContext<TRequest>((TRequest)request);
				var failures = validators.Select(v => v.Validate(context))
										 .SelectMany(result => result.Errors)
										 .Where(f => f != null)
										 .ToList();

				if (failures.Count != 0)
				{
					throw new ValidationException(failures);
				}
			}
		}
	}
}
