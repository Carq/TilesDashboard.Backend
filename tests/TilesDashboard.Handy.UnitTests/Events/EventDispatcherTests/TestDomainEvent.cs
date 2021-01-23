using TilesDashboard.Handy.Events;

namespace TilesDashboard.Handy.UnitTests.Events.EventDispatcherTests
{
    public class TestDomainEvent : IEvent
    {
        public int EventId { get; set; }
    }
}
