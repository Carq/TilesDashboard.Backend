using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using TilesDashboard.Handy.Events;
using TilesDashboard.TestUtils;

namespace TilesDashboard.Handy.UnitTests.Events.EventDispatcherTests
{
    internal class PublishAsyncTests : TestBase<EventDispatcher>
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenEventIsNull()
        {
            // given & when
            Func<Task> action = async () =>
                                {
                                    await TestCandidate.PublishAsync<TestDomainEvent>(null, CancellationToken.None);
                                };

            // then
            action.Should().ThrowExactlyAsync<ArgumentNullException>();
        }

        [Test]
        public async Task ShouldThrowException_WhenThereIsNoCommandHandlerForGivenCommand()
        {
            // given
            var componentContext = A.Fake<IComponentContext>();
            A.CallTo(() => componentContext.ComponentRegistry).Returns(A.Fake<IComponentRegistry>());
            AutoFake.Provide(componentContext);

            var logger = A.Fake<ILogger<EventDispatcher>>();
            AutoFake.Provide(logger);


            // when
            await TestCandidate.PublishAsync(new TestDomainEvent(), CancellationToken.None);

            // then
            A.CallTo(logger).Where(x => x.Method.Name == "Log").MustHaveHappened();
        }
    }
}
