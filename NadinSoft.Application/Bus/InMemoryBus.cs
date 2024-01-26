using MediatR;
using System.ComponentModel.DataAnnotations;

namespace NadinSoft.Application.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T @event) where T : INotification
        {
            await _mediator.Publish(@event);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : IRequest<ValidationResult>, IBaseRequest
        {
            return await _mediator.Send(command);
        }
    }
}
