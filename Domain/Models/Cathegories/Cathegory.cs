using System;
using System.Collections.Generic;
using Domain.Models.Entities;

namespace Domain.Models.Cathegories
{
    public class Cathegory : Entity, IEntityCathegory
    {
        public string IdMLB { get ; set; }
        public string NameMLB { get; set; }
        public List<CathegoryChild> CathegoriesChildren { get; set; }

        public Cathegory(string idFromMLB, string nameMLB)
        {
            Id = Guid.NewGuid();
            IdMLB = idFromMLB;
            NameMLB = nameMLB;
        }
    }
}