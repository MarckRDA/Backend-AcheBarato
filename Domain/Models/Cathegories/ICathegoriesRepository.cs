using System.Collections.Generic;
using Domain.Infra;

namespace Domain.Models.Cathegories
{
    public interface ICathegoryRepository : IRepository<Cathegory>
    {
        List<CathegoryChild> GetCathegoryChildren(string idMLBCathegory);
        List<Cathegory> GetCathegories();

    }
}