using System.Collections.Generic;

namespace Domain.Models.Cathegories
{
    public class CathegoryDTO
    {
        public string IdMLB { get ; set; }
        public string NameMLB { get; set; }
        public List<CathegoryChild> CathegoriesChildren { get; set; }
    }
}