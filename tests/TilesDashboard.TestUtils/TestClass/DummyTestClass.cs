namespace TilesDashboard.TestUtils.TestClass
{
    public class DummyTestClass
    {
        public DummyTestClass(int id, string name, DummyTestEnum someEnum)
        {
            Id = id;
            Name = name;
            SomeEnum = someEnum;
        }

        public DummyTestClass(int id, string name, DummyTestEnum someEnum, DummyTestClass dummyChild) : this(id, name, someEnum)
        {
            DummyChild = dummyChild;
        }

        public int Id { get; }

        public string Name { get; }

        public DummyTestEnum SomeEnum { get; }

        public DummyTestClass DummyChild { get; }
    }
}
