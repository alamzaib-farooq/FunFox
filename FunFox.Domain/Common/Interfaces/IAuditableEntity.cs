namespace FunFox.Domain.Common.Interfaces
{
    public interface IAuditableEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateModified { get; set; }
        public long CreatedTime { get; set; }
    }
}
