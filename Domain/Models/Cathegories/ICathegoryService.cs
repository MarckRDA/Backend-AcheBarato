using System.Collections.Generic;

namespace Domain.Models.Cathegories
{
    public interface ICathegoryService
    {
        List<CathegoryDTO> GetCathegories();
        List<CathegoryChildDTO> GetCathegoryChildren(string idMLBCathegory);
    }
}