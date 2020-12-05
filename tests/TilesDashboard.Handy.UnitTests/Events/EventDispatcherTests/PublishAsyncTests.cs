using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TilesDashboard.Handy.UnitTests.Events.EventDispatcherTests;
using FluentAssertions;
using TilesDashboard.TestUtils;
using System.Threading;
using Autofac;
using FakeItEasy;
using Autofac.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.Handy.Events
{
    internal class PublishAsyncTests : TestBase<EventDispatcher>
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenEventIsNull()
        {
            // Act
            Func<Task> action = async () =>
                                {
                                    await TestCandidate.PublishAsync<TestDomainEvent>(null, CancellationToken.None);
                                };

            // Assert
            action.Should().Throw<ArgumentNullException>();
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
