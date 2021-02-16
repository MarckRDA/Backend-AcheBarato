using System.Collections.Generic;

namespace Domain.Models.Cathegories
{
    public class Cathegory
    {
        public string IdMLB { get; private set; }
        public string Name { get; private set; }
      
        public Cathegory(string idMLB, string name)
        {
            IdMLB = idMLB;
            Name = name;
        }
    }
}