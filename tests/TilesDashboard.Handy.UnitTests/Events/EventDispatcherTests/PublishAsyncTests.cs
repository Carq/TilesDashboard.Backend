﻿using NUnit.Framework;
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

namespace TilesDashboard.Handy.Events
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
