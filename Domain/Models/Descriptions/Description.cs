namespace Domain.Models.Descriptions
{
    public class Description 
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public Description(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}