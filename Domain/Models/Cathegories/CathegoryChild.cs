namespace Domain.Models.Cathegories
{
    public class CathegoryChild : IEntityCathegory
    {
        public string IdMLB { get; private set;}
        public string NameMLB { get; private set;}

        public CathegoryChild(string idMLB, string nameMLB)
        {
            IdMLB = idMLB;
            NameMLB = nameMLB;
        }
    }
}