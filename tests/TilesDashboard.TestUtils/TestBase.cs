using Autofac.Extras.FakeItEasy;
using NUnit.Framework;

namespace TilesDashboard.TestUtils
{
    public abstract class TestBase<TTestClass>
        where TTestClass : class
    {
        protected AutoFake AutoFake { get; private set; }

        protected TTestClass TestCandidate => Resolve<TTestClass>();

        [SetUp]
        public virtual void SetUp()
        {
            AutoFake = new AutoFake();
        }

        [TearDown]
        public virtual void Cleanup()
        {
            AutoFake?.Dispose();
        }

        public TService Register<TService>(TService instance)
            where TService : class
        {
            return AutoFake.Provide(instance);
        }

        protected TDependency Resolve<TDependency>()
        {
            return AutoFake.Resolve<TDependency>();
        }

        protected TDependency M<TDependency>()
        {
            return AutoFake.Resolve<TDependency>();
        }
    }
}
