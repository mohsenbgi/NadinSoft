using MediatR;
using NadinSoft.Domain.Common;

namespace Final_NadinSoft.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
