using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Models.Cathegories
{
    public class CathegoryRepository : ICathegoryRepository
    {
        private readonly IRepository<Cathegory> _cathegoryRepository;       

        public CathegoryRepository(IRepository<Cathegory> repository)
        {
            _cathegoryRepository = repository;
        }

        public void add(Cathegory cathegory)
        {
            _cathegoryRepository.add(cathegory);
        }

        public List<Cathegory> GetCathegories()
        {
            using (var db = new AcheBaratoContext())
            {
                return db.Cathegories.ToList();
            }
        }

        public List<CathegoryChild> GetCathegoryChildren(string idMLBCathegory)
        {
            using (var db = new AcheBaratoContext())
            {
                return db.Cathegories.FirstOrDefault(cathegoriesChild => cathegoriesChild.IdMLB == idMLBCathegory).CathegoriesChildren.ToList();
            }
        }

        public Cathegory GetElement(Func<Cathegory, bool> predicate)
        {
            return _cathegoryRepository.GetElement(predicate);
        }
    }
}