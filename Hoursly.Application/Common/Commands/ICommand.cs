using MediatR;

namespace Hoursly.Application.Common.Commands
{
    public interface ICommand : IRequest<Unit>
    {
    }
}