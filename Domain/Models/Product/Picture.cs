using System;
using Domain.Models.Entities;

namespace Domain.Models.Products
{
    public class Picture : Entity
    {
        public string LinkPicture { get; set; }

        public Picture()
        {
            Id = Guid.NewGuid();
        }
    }
}