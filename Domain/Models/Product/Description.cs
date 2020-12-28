using Domain.Models.Entities;

namespace Domain.Models.Products
{
    public class Description : Entity
    {
        public string IdMLB { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Description(string idMLB, string name, string value)
        {
            IdMLB = idMLB;
            Name = name;
            Value = value;
        }
    }
}