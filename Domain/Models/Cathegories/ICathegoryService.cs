using System.Collections.Generic;
using Domain.ApiMLBConnection.Consumers;

namespace Domain.Models.Cathegories
{
    public interface ICathegoryService
    {
        

        List<CathegoryDTO> GetCathegories();
        List<CathegoryChildDTO> GetCathegoryChildren(string idMLBCathegory);


    }
}