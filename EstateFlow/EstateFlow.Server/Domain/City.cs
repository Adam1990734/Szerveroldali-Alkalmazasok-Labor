using System.Text.Json.Serialization;

namespace EstateFlow.Server.Domain
{
    public class City
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
