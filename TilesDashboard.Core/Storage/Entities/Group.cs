namespace TilesDashboard.Core.Storage.Entities
{
    public class Group
    {
        public Group(string name)
        {
            Name = name;
            Order = string.IsNullOrEmpty(name) ? 100 : 1;
        }

        public string Name { get; }

        public int Order { get; }
    }
}
