using System.Collections.Generic;
using Domain.ApiMLBConnection.Consumers;

namespace Domain.Models.Cathegories
{
    public class CathegoryService : ICathegoryService
    {
        private readonly ICathegoryRepository _cathegoryRepository;

        public CathegoryService(ICathegoryRepository repository)
        {
            _cathegoryRepository = repository;
        }

        public List<CathegoryDTO> GetCathegories()
        {
            PostCathgoriesInDB();
            var cathegories = _cathegoryRepository.GetCathegories();
            
            var cathegoriesDTOList = new List<CathegoryDTO>();

            foreach (var cathegory in cathegories)
            {
                cathegoriesDTOList.Add(new CathegoryDTO()
                                        {
                                            IdMLB = cathegory.IdMLB,
                                            NameMLB = cathegory.NameMLB
                                        });
            } 
            
            return cathegoriesDTOList;
        }

        public List<CathegoryChildDTO> GetCathegoryChildren(string idMLBCathegory)
        {
            var cathegoryChildrenList = _cathegoryRepository.GetCathegoryChildren(idMLBCathegory);
            var cathegoryChildrenDTOList = new List<CathegoryChildDTO>();

            foreach (var cathegoryChild in cathegoryChildrenList)
            {
                cathegoryChildrenDTOList.Add(new CathegoryChildDTO()
                                            {
                                                IdMLB = cathegoryChild.IdMLB,
                                                NameMLB = cathegoryChild.NameMLB
                                            });
            }

            return cathegoryChildrenDTOList;
        }

        private void PostCathgoriesInDB()
        {
            var cathegories = ApiMLB.GetCathegories();

            foreach (var cathegory in cathegories)
            {
                if(_cathegoryRepository.GetElement(cth => cth.IdMLB == cathegory.IdMLB) != null)
                {
                    cathegories.Remove(cathegory);
                }
            }

            foreach (var cathegory in cathegories)
            {
                _cathegoryRepository.add(cathegory);
            }

        }
    }
}