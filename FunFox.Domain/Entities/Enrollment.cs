using FunFox.Domain.Common;

namespace FunFox.Domain.Entities
{
    public class Enrollment : BaseAuditableEntity
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
    }
}
