using System;

namespace Models.Entities
{
    public abstract class Base
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}