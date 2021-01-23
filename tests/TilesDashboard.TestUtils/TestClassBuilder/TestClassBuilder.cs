namespace TilesDashboard.TestUtils.TestClassBuilder
{
    public class TestClassBuilder<T>
    {
        protected T Item { get; set; }

        public T Build()
        {
            return Item;
        }
    }
}
