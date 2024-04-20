using FunFox.Domain.Common.Interfaces;

namespace FunFox.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateModified { get; set; }
        public long CreatedTime { get; set; }
    }
}
