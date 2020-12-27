using System;

namespace Domain.Models.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = new Guid();
    }
}