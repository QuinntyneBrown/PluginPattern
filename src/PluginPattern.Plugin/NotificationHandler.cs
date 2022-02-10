using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PluginPattern.SharedKernal;
using System.Threading;
using System.Threading.Tasks;

namespace PluginPattern.Plugin
{
    public class NotificationHandler : INotificationHandler<Notification>
    {
        private readonly IService _service;

        public NotificationHandler(IService service)
        {
            _service = service;
        }

        public Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            _service.Process(notification);

            return Task.CompletedTask;
        }
    }
}
