namespace SharedDomain.Models
{
    public class PersonModel
    {
        //EF Constructor
        protected PersonModel() { }

        public PersonModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return $"[{Id}] -> ({Name})";
        }
    }
}