using System;
using System.Collections.Generic;

namespace Domain.src.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }        
        public bool EValido { get; set; }        
        public List<string> Error { get; set; }

        public UserDTO(Guid idUser)
        {
           EValido = true;
           Id = idUser;
        }

        public UserDTO(List<string> erros)
        {
             Error = erros;
        }
    }    
}