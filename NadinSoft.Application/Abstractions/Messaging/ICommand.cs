using MediatR;
using NadinSoft.Domain.Common;

namespace NadinSoft.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
