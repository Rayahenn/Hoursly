using System.Threading;
using System.Threading.Tasks;
using Hoursly.Domain.Common;
using MediatR;

namespace Hoursly.Application.Common
{
    internal class UnitOfWorkDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        internal UnitOfWorkDecorator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (request is IQuery) return await next();

            var response = await next();
            await _unitOfWork.CommitAsync(cancellationToken);

            return response;
        }
    }
}