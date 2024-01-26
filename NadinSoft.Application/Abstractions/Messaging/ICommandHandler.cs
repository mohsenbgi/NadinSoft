using MediatR;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Common;

namespace Final_NadinSoft.Application.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
     where TCommand : ICommand
    {
    }

    public interface ICommandHandler<TCommand, TResponse>
        : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }

}
