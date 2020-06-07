namespace TilesDashboard.Contract
{
    public class GroupDto
    {
        public GroupDto(string name, int? order)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; set; }

        public int? Order { get; set; }
    }
}
