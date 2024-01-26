using MediatR;
using System.ComponentModel.DataAnnotations;

namespace NadinSoft.Application.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : INotification;

        Task<ValidationResult> SendCommand<T>(T command) where T : IRequest<ValidationResult>, IBaseRequest;
    }
}
