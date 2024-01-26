using MediatR;
using NadinSoft.Domain.Common;

namespace Final_NadinSoft.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    {
    }
}
