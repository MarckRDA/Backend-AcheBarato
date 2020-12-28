using Domain.Models.Entities;

namespace Domain.Models.Cathegories
{
    public class CathegoryChild : Entity, IEntityCathegory
    {
        public string IdMLB { get; private set;}
        public string NameMLB { get; private set;}
        
    }
}