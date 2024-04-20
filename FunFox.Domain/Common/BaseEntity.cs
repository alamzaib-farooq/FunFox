using FunFox.Domain.Common.Interfaces;

namespace FunFox.Domain.Common
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
