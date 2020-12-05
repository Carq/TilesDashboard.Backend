using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;

namespace TilesDashboard.Handy.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _resolver;

        private readonly ILogger<EventDispatcher> _logger;

        public EventDispatcher(IComponentContext resolver, ILogger<EventDispatcher> logger)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishAsync<TEvent>(TEvent eventBody, CancellationToken cancellationToken)
            where TEvent : IEvent
        {
            if (eventBody == null)
            {
                throw new ArgumentNullException(nameof(eventBody));
            }

            var handlers = _resolver.ResolveOptional<IEnumerable<IEventHandler<TEvent>>>() ?? Array.Empty<IEventHandler<TEvent>>();
            if (!handlers.Any())
            {
                _logger.LogWarning($"Event {eventBody.GetType().Name} has no Event Handlers.");
                return;
            }

            foreach (var handler in handlers)
            {
                await handler.ExecuteAsync(eventBody, cancellationToken);
            }
        }
    }
}
