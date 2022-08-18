using System.Text.Json;

namespace SharedDomain.Models
{
    public enum OccurrencyType
    {
        CreatePerson = 1
    }

    public class OccurrencyModel
    {
        //EF Constructor
        protected OccurrencyModel() { }

        public OccurrencyModel(OccurrencyType type, object json)
        {
            Id = Guid.NewGuid();
            Type = type;
            Json = JsonSerializer.Serialize(json);
        }

        public Guid Id { get; private set; }
        public OccurrencyType Type { get; private set; }
        public string Json { get; private set; }

        public override string ToString()
        {
            return $"[{Id} - {Type} - Content: {Json}]";
        }

    }
}